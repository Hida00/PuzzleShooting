using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    //最大体力と現在の体力
    //int player_maxHP = n;
    //int player_nowHP;

    public Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        //体力を満タンにする
        slider.value = 1;
        //現在の体力と最大体力を同期
        //player_nowHP = player_maxHP;
        //Debug.Log("start nowHP = maxHP:" + player_nowHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
