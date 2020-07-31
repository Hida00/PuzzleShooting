using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HPbarctrl : MonoBehaviour
{

    //HPの値の変数
    //float maxHP = a;
    //float nowHP;

    //Sliderを入れる
    public Slider slider;

    void Start()
    {
        slider.value = 1;
        //nowHP = maxHP;
    }

    void Update()
    {
        //HPの値
        //float _hp = [適当な変数];
    }
}
