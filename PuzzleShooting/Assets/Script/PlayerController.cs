using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //プレイヤーの弾丸
    public GameObject PlayerBullet;

    //Shift押下中速度を落とすための補正倍率
    public float speedMag = 1.0f;

    float speed = 0.15f;

    //プレイヤーの体力
    public float health_Point = 100f;

    public float strength;
    float damage = 15f;
    float moveright = 1f;
    float moveleft = 1f;
    float moveup = 1f;
    float movedown = 1f;

    int framecount = 0;
    int attackSpeed = 10;

    bool skill = false;

    void Start()
    {
        strength = 1.0f;
    }

    void Update()
    {
        Vector3 PlayerPosition = GameObject.Find("Player").GetComponent<Transform>().position;
        var one = new Vector3(PlayerPosition.x - 1f , PlayerPosition.y + 0.5f , PlayerPosition.z);
        var two = new Vector3(PlayerPosition.x + 1f , PlayerPosition.y + 0.5f , PlayerPosition.z);
        var three = new Vector3(PlayerPosition.x , PlayerPosition.y + 1f , PlayerPosition.z);
        framecount++;

        if(framecount >= attackSpeed)
        {
            framecount = 0;
            var obj = Instantiate(PlayerBullet , one , Quaternion.identity);
            obj.GetComponent<BulletController>().damagePoint = strength * damage;
            obj = Instantiate(PlayerBullet , two , Quaternion.identity);
            obj.GetComponent<BulletController>().damagePoint = strength * damage;
            obj = Instantiate(PlayerBullet , three , Quaternion.identity);
            obj.GetComponent<BulletController>().damagePoint = strength * damage;
        }

        //移動キーの取得
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            this.transform.position += Vector3.down * -1f * speed * speedMag * moveup;
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            this.transform.position += Vector3.down * +1f * speed * speedMag * movedown;
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            this.transform.position += Vector3.left * +1f * speed * speedMag * moveleft;
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            this.transform.position += Vector3.right * 1f * speed * speedMag * moveright;

        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedMag = 0.3f;
        }
        if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedMag = 1.0f;
        }

        if(health_Point <= 0.00f)
        {
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
    void PositionOver(Vector3 position)
    {
        if(position.x >= 9f) moveright = 0f;
        else moveright = 1f;
        if(position.x <= -9f) moveleft = 0f;
        else moveleft = 1f;
        if(position.y >= 9f) moveup = 0f;
        else moveup = 1f;
        if(position.y <= -9f) movedown = 0f;
        else movedown = 1f;
    }
}
