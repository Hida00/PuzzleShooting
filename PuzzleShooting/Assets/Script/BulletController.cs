using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 0.1f;

     public float damagePoint = 1.0f;

    PlayerController _playerController;

    public bool isPlayer;

    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if(isPlayer)
        {
            this.transform.position += transform.up * speed * Time.deltaTime * 0.6f;
        }
        else
        {
            this.transform.position += transform.up * speed * Time.deltaTime * 0.3f;
        }

        if (this.transform.position.y >= 15f || this.transform.position.y <= -15f || this.transform.position.x <= -20f || this.transform.position.x >= 20f)
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
            _playerController.health_Point -= 5f;
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
    }
}

