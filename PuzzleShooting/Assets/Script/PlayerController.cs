using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //プレイヤーの弾丸
    public GameObject PlayerBullet;
    public GameObject TrackingBullet;
    public Image playerImage;

    //Shift押下中速度を落とすための補正倍率
    public float speedMag = 1.0f;
    readonly float speed = 0.15f;

    //プレイヤーの体力
    public float health_Point = 1000f;
    public float maxHealth;

    public float strength;
    public float defence;
    readonly float damage = 25f;
    float moveright = 1f;
    float moveleft = 1f;
    float moveup = 1f;
    float movedown = 1f;
    float moveOverY;

    int framecount = 0;
    int attackSpeed = 15;

    bool skill = false;
    bool isShift = false;

    void Start()
    {
        maxHealth = health_Point;

        strength = 1.0f;
        defence = 1.0f;

        moveOverY = 19.2f * ((float)Screen.height / (float)Screen.width);
    }

    void Update()
    {
        framecount++;

        playerImage.transform.position
            = RectTransformUtility.WorldToScreenPoint(Camera.main , this.transform.position) + Vector2.up * 2;

        if(framecount >= attackSpeed && !GameObject.Find("PanelController").GetComponent<PanelController>().isSkill)
        {
            Vector3 PlayerPosition = GameObject.Find("Player").GetComponent<Transform>().position;
            var one = new Vector3(PlayerPosition.x - 1f , PlayerPosition.y + 0.5f , PlayerPosition.z);
            var two = new Vector3(PlayerPosition.x + 1f , PlayerPosition.y + 0.5f , PlayerPosition.z);
            var three = new Vector3(PlayerPosition.x - 0.5f , PlayerPosition.y + 1f , PlayerPosition.z);
            var four = new Vector3(PlayerPosition.x , PlayerPosition.y + 1.5f , PlayerPosition.z);
            var five = new Vector3(PlayerPosition.x + 0.5f , PlayerPosition.y + 1f , PlayerPosition.z);

            if(!isShift)
            {
                var obj = Instantiate(PlayerBullet , three , Quaternion.Euler(0 , 0 , 5f));
                obj.GetComponent<BulletController>().damagePoint = strength * damage;
                obj = Instantiate(PlayerBullet , four , Quaternion.identity);
                obj.GetComponent<BulletController>().damagePoint = strength * damage;
                obj = Instantiate(PlayerBullet , five , Quaternion.Euler(0 , 0 , -5f));
                obj.GetComponent<BulletController>().damagePoint = strength * damage;
                obj = Instantiate(TrackingBullet , one , Quaternion.Euler(0 , 0 , 15f));
                obj.GetComponent<BulletController>().damagePoint = strength * damage / 2f;
                obj = Instantiate(TrackingBullet , two , Quaternion.Euler(0 , 0 , -15f));
                obj.GetComponent<BulletController>().damagePoint = strength * damage / 2f;
            }
            else if(isShift)
            {
                var obj = Instantiate(PlayerBullet , three , Quaternion.identity);
                obj.GetComponent<BulletController>().damagePoint = strength * damage;
                obj = Instantiate(PlayerBullet , four , Quaternion.identity);
                obj.GetComponent<BulletController>().damagePoint = strength * damage;
                obj = Instantiate(PlayerBullet , five , Quaternion.identity);
                obj.GetComponent<BulletController>().damagePoint = strength * damage;
            }

            framecount = 0;
        }
        float skill = GameObject.Find("PanelController").GetComponent<PanelController>().skillSpeed;
        //移動キーの取得
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            this.transform.position += Vector3.down * -1f * speed * speedMag * moveup * skill;
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            this.transform.position += Vector3.down * +1f * speed * speedMag * movedown * skill;
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            this.transform.position += Vector3.left * +1f * speed * speedMag * moveleft * skill;
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            this.transform.position += Vector3.right * 1f * speed * speedMag * moveright * skill;

        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedMag = 0.3f;
            isShift = true;
        }
        if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedMag = 1.0f;
            isShift = false;
        }

        if(health_Point <= 0.00f)
        {
            GameObject.Find("GameController").GetComponent<GameController>().FinishGame(false);
            SceneManager.LoadScene("Result");
        }

        PositionOver(this.transform.position);
    }
    public void Invisible()
    {
        if(skill)
        {
            this.gameObject.GetComponentInChildren<CapsuleCollider>().isTrigger = true;
            skill = false;

            playerImage.color = new Color(1f , 1f , 1f , 1f);
        }
        else
        {
            this.gameObject.GetComponentInChildren<CapsuleCollider>().isTrigger = false;
            skill = true;
            Invoke("Invisible" , 15f);

            playerImage.color = new Color(1f , 1f , 1f , 0.4f);
        }
    }
    public void AttackSpeed()
    {
        if(skill)
        {
            attackSpeed = 15;
            framecount = 0;
            skill = false;
        }
        else
        {
            attackSpeed = 7;
            framecount = 0;
            skill = true;
            Invoke("AttackSpeed" , 15f);
        }
    }
    public void RateDamage()
    {
        var Enemys = GameObject.FindGameObjectsWithTag("ENEMY");

        if(Enemys.Length != 0)
        {
            foreach(var obj in Enemys)
            {
                obj.GetComponent<Viran>().ViranHealth -= obj.GetComponent<Viran>().ViranHealth * 0.40f;
            }
        }

        var MidBoss = GameObject.FindGameObjectsWithTag("MIDBOSS");

        if(MidBoss.Length != 0)
        {
            foreach(var obj in MidBoss)
            {
                obj.GetComponent<MidBoss>().HealthPoint -= obj.GetComponent<MidBoss>().HealthPoint * 0.40f;
            }
        }

        var Boss = GameObject.FindGameObjectsWithTag("BOSS");

        if(Boss.Length != 0)
        {
            foreach(var obj in Boss)
            {
                obj.GetComponent<Boss>().bossHealth -= obj.GetComponent<Boss>().bossHealth * 040f;
            }
        }
    }
    public void Strength()
    {
        if(skill)
        {
            strength = 1.0f;
            skill = false;
        }
        else
        {
            strength = 2.0f;
            skill = true;
            Invoke("Strength" , 15f);
        }
    }
    public void Defence()
    {
        if(skill)
        {
            defence /= 2f;
            skill = false;
        }
        else
        {
            defence *= 2f;
            skill = true;
            Invoke("Defence" , 15f);
        }
    }
    void PositionOver(Vector3 position)
    {
        if(position.x >= 12f) moveright = 0f;
        else moveright = 1f;
        if(position.x <= -12f) moveleft = 0f;
        else moveleft = 1f;
        if(position.y >= moveOverY) moveup = 0f;
        else moveup = 1f;
        if(position.y <= -moveOverY) movedown = 0f;
        else movedown = 1f;
    }
}
