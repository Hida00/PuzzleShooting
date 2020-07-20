using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHP : MonoBehaviour
{
    //最大HPと現在のHP
    //int player_maxHP = n;
    //int player_nowHP;

    //Sliderを読み込ませる
    public Slider slider;

    PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        //HPを満タンにする
        slider.value = 1;
        //現在のHPと最大HPを同期
        //player_nowHP = player_maxHP;
        //Debug.Log("start now = max:" + player_nowHP);

        float width = Screen.width / 2f * 0.7f;
        float height = Screen.height / 2f * 0.7f;
        slider.transform.localPosition = new Vector3(- width , -height / 2f , 0);

        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = _playerController.health_Point / 100f;
    }
}
