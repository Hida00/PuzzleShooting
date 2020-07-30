using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //プレイヤーの弾丸
    public GameObject PlayerBullet;

    //Shift押下中速度を落とすための補正倍率
    float speedMag = 1.0f;

    float speed = 0.15f;

    //プレイヤーの体力
    public float health_Point = 100f;

    public float strength;

    int framcount = 0;

    void Start()
    {
        strength = 1.0f;
    }

    void Update()
    {
        Vector3
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
        
        //移動キーの取得
        if (Input.GetKey(KeyCode.W)) this.transform.position += Vector3.down * -1f * speed * speedMag;
        if (Input.GetKey(KeyCode.S)) this.transform.position += Vector3.down * +1f * speed * speedMag;
        if (Input.GetKey(KeyCode.A)) this.transform.position += Vector3.left * +1f * speed * speedMag;
        if (Input.GetKey(KeyCode.D)) this.transform.position += Vector3.right * 1f * speed * speedMag;
        if (Input.GetKey(KeyCode.UpArrow)) this.transform.position += Vector3.down * -1f * speed * speedMag;
        if (Input.GetKey(KeyCode.DownArrow)) this.transform.position += Vector3.down * +1f * speed * speedMag;
        if (Input.GetKey(KeyCode.LeftArrow)) this.transform.position += Vector3.left * +1f * speed * speedMag;
        if (Input.GetKey(KeyCode.RightArrow)) this.transform.position += Vector3.right * 1f * speed * speedMag;
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftShift)) speedMag = 0.3f;
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftShift)) speedMag = 1.0f;

        if(health_Point <= 0.00f)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
