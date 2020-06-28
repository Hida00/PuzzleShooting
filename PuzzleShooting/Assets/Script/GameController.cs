using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //プレイヤーの弾丸、BulletControllerのisPlayerがtrue
    public GameObject PlayerBullet;

    //自弾の発射位置つまりプレイヤーの位置
    [NonSerialized]
    public Vector3 PlayerPosition;

    //弾の発射間隔
    int framcount = 0;

    void Start()
    {
        //FPSを60で固定
        Application.targetFrameRate = 65;
    }

    void Update()
    {
        //プレイヤーの位置を取得してその左右から弾を発射
        PlayerPosition = GameObject.Find("Player").GetComponent<Transform>().position;
        var one = new Vector3(PlayerPosition.x - 1f , PlayerPosition.y + 0.5f , PlayerPosition.z);
        var two = new Vector3(PlayerPosition.x + 1f , PlayerPosition.y + 0.5f , PlayerPosition.z);

        //毎フレームで発射すると多いので秒間6回、処理によっては減らす
        framcount++;
        if(framcount == 10)
        {
            framcount = 0;
            //弾を生成
            Instantiate(PlayerBullet , one , Quaternion.identity);
            Instantiate(PlayerBullet , two , Quaternion.identity);
        }
    }
}
