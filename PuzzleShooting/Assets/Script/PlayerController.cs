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
        if(!GameObject.Find("PanelController").GetComponent<PanelController>().isSkill)
        {
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
        }

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
        }
        else
        {
            this.gameObject.GetComponentInChildren<CapsuleCollider>().isTrigger = false;
            skill = true;
            Invoke("Invisible" , 10f);
        }
    }
    public void AttackSpeed()
    {
        if(skill)
        {
            attackSpeed = 10;
            framecount = 0;
            skill = false;
        }
        else
        {
            attackSpeed = 5;
            framecount = 0;
            skill = true;
            Invoke("AttackSpeed" , 10f);
        }
    }
    public void RateDamage()
    {
        var Enemys = GameObject.FindGameObjectsWithTag("ENEMY");

        if(Enemys.Length != 0)
        {
            int length = Enemys.Length;
            System.Random r = new System.Random();
            
            for(int i = 0; i < 4; i++)
            {
                int num = r.Next(0 , length);
                Enemys[num].GetComponent<Viran>().ViranHealth -= Enemys[num].GetComponent<Viran>().ViranHealth * 0.5f;
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
            Invoke("Strength" , 10f);
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
            Invoke("Defence" , 10f);
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
