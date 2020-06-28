using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //スピード、要調整
    public float speed = 12f;

    //弾の角度
    float angleZ;

    PlayerController _playerController;

    //プレイヤーが発射した弾かを判別するためのもの
    public bool isPlayer;

    void Start()
    {
        //プレイヤーObjectを取得
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        //発射するObjectの角度を取得
        angleZ = this.transform.eulerAngles.z;
        this.transform.position += transform.up * speed * (float)Math.Cos(angleZ * (Math.PI / 180)) * Time.deltaTime;
        this.transform.position += transform.up * speed * (float)Math.Sin(angleZ * (Math.PI / 180)) * Time.deltaTime;

        if (this.transform.position.y >= 15f || this.transform.position.y <= -15f || this.transform.position.x <= -20f || this.transform.position.x >= 20f)
        { 
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //当たったもののタグがPLAYERでプレイヤーの発射した弾でない
        if(other.gameObject.tag == "PLAYER" && !isPlayer)
        {
            //ここにプレイヤーが弾に当たった時の処理を書く
            _playerController.health_Poiint -= 10f;
        }
    }
}
