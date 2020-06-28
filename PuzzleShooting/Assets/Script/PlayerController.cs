using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Shift押下中速度を落とすための補正倍率
    float speedMag = 1.0f;

    float speed = 0.15f;

    //プレイヤーの体力
    [NonSerialized]
    public float health_Poiint = 100f;

    void Start()
    {
        
    }

    void Update()
    {
        //プレイヤー移動
        if (Input.GetKey(KeyCode.W)) this.transform.position += Vector3.down * -1f * speed * speedMag;
        if (Input.GetKey(KeyCode.S)) this.transform.position += Vector3.down * +1f * speed * speedMag;
        if (Input.GetKey(KeyCode.A)) this.transform.position += Vector3.left * +1f * speed * speedMag;
        if (Input.GetKey(KeyCode.D)) this.transform.position += Vector3.right * 1f * speed * speedMag;
        if (Input.GetKey(KeyCode.UpArrow)) this.transform.position += Vector3.down * -1f * speed * speedMag;
        if (Input.GetKey(KeyCode.DownArrow)) this.transform.position += Vector3.down * +1f * speed * speedMag;
        if (Input.GetKey(KeyCode.LeftArrow)) this.transform.position += Vector3.left * +1f * speed * speedMag;
        if (Input.GetKey(KeyCode.RightArrow)) this.transform.position += Vector3.right * 1f * speed * speedMag;

        //Shiftを押すと倍率が変わって移動距離が小さくなる、話すと元に戻る
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftShift)) speedMag = 0.3f;
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftShift)) speedMag = 1.0f;

        //体力がゼロになったら終了
        if(health_Poiint <= 0.00f)
        {
            //終了時の処理(シーン変更など)をここに書く
            //仮にDebugが終了するようにしてある
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
