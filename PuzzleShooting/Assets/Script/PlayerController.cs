using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //プレイヤーobject
    public GameObject Player;

    //Shift押下中速度を落とすための倍率補正
    float speedMag = 1.0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) Player.transform.position += Vector3.down * -1f * 0.1f * speedMag;
        if (Input.GetKey(KeyCode.S)) Player.transform.position += Vector3.down * +1f * 0.1f * speedMag;
        if (Input.GetKey(KeyCode.A)) Player.transform.position += Vector3.left * +1f * 0.1f * speedMag;
        if (Input.GetKey(KeyCode.D)) Player.transform.position += Vector3.right * 1f * 0.1f * speedMag;
        if (Input.GetKey(KeyCode.UpArrow)) Player.transform.position += Vector3.down * -1f * 0.1f * speedMag;
        if (Input.GetKey(KeyCode.DownArrow)) Player.transform.position += Vector3.down * +1f * 0.1f * speedMag;
        if (Input.GetKey(KeyCode.LeftArrow)) Player.transform.position += Vector3.left * +1f * 0.1f * speedMag;
        if (Input.GetKey(KeyCode.RightArrow)) Player.transform.position += Vector3.right * 1f * 0.1f * speedMag;
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftShift)) speedMag = 0.3f;
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftShift)) speedMag = 1.0f;

    }
}
