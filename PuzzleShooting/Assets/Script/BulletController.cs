using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    PlayerController _playerController;

    public float speed;// = 0.1f;
    public float damagePoint = 1.0f;
    float moveOverY;

    public bool isBoss = false;
    public bool isPlayer;
    public bool isTracking;

    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        moveOverY = (19.2f * ((float)Screen.height / (float)Screen.width)) + 1f;
    }

    void Update()
    {
        if(isTracking)
        {
            if(isPlayer)
            {
                this.transform.position += transform.up * speed * Time.deltaTime * 0.7f;
                try
                {
                    var Enemy = GameObject.FindGameObjectsWithTag("ENEMY");
                    float angle = (float)Math.Atan2(this.transform.position.y - Enemy[0].transform.position.y , this.transform.position.x - Enemy[0].transform.position.x) * 180f / (float)Math.PI;
                    this.transform.rotation = Quaternion.Euler(0 , 0 , angle + 90f);
                    //this.transform.position += new Vector3((float)Math.Sin(angle) * speed * Time.deltaTime , (float)Math.Cos(angle) * speed * Time.deltaTime , 0) * 0.7f;
                    this.transform.position += transform.up * speed * Time.deltaTime * 0.5f;
                }
                catch
                {
                    this.transform.position += transform.up * speed * Time.deltaTime * 0.7f;
                }
            }
        }
        else
        {
            if(isPlayer)
            {
                this.transform.position += transform.up * speed * Time.deltaTime * 0.6f;
            }
            else
            {
                if(!isBoss) this.transform.position += transform.up * speed * Time.deltaTime * 0.3f;
            }
        }

        if (this.transform.position.y >= moveOverY || this.transform.position.y <= -moveOverY || this.transform.position.x <= -13f || this.transform.position.x >= 13f)
        { 
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PLAYER" && !isPlayer)
        {
            //ここにプレイヤーが弾に当たった時の処理を書く
            //Debug.Log("Hit!");
            _playerController.health_Point -= damagePoint - _playerController.defence;
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "ENEMY" && isPlayer)
        {
            //Debug.Log("Hit");
            float defense = other.gameObject.GetComponent<Viran>().defensePoint;
            other.gameObject.GetComponent<Viran>().ViranHealth -= (damagePoint - defense);
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "BOSS" && isPlayer)
        {
            float defence = other.gameObject.GetComponent<Boss>().defensePoint;
            other.gameObject.GetComponent<Boss>().bossHealth -= (damagePoint - defence);
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "MIDBOSS" && isPlayer)
        {
            float defence = other.gameObject.GetComponent<MidBoss>().defencePoint;
            other.gameObject.GetComponent<MidBoss>().HealthPoint -= (damagePoint - defence);
            Destroy(this.gameObject);
        }
    }
}

