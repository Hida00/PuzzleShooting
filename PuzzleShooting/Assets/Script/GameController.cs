using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //プレイヤーの弾丸
    public GameObject PlayerBullet;

    //自弾の発射位置つまりプレイヤーの位置
    [NonSerialized]
    public Vector3 PlayerPosition;

    //弾の発射間隔
    int framcount = 0;

    string fileName;

    void Start()
    {
        Application.targetFrameRate = 65;
    }

    void Update()
    {
        PlayerPosition = GameObject.Find("Player").GetComponent<Transform>().position;
        var one = new Vector3(PlayerPosition.x - 1f , PlayerPosition.y + 0.5f , PlayerPosition.z);
        var two = new Vector3(PlayerPosition.x + 1f , PlayerPosition.y + 0.5f , PlayerPosition.z);
        framcount++;

        if(framcount == 10)
        {
            framcount = 0;
            Instantiate(PlayerBullet , one , Quaternion.identity);
            Instantiate(PlayerBullet , two , Quaternion.identity);
        }
    }
    public void Set()
    {
        fileName = SelectController.SelectName;
    }
}
