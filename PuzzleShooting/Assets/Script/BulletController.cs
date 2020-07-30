using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 12f;

    float angleZ;

    PlayerController _playerController;

    public bool isPlayer;

    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        angleZ = this.transform.eulerAngles.z;
        this.transform.position += transform.up * speed * Time.deltaTime;

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
            _playerController.health_Point -= 10f;
        }
        if(other.gameObject.tag == "ENEMY" && isPlayer)
        {
            //Debug.Log("Hit");
        }
    }
}

