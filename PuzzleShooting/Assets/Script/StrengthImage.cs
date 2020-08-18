using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StrengthImage : MonoBehaviour
{
    Strength skill;

    PanelController _panelController;

    [NonSerialized]
    public int Num;

    void Start()
    {
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
        skill = GameObject.Find("Strength(Clone)").GetComponent<Strength>();
    }

    void Update() { }

    public void Image_Click()
    {
        if(skill.isClick)
        {
            skill.DrawLine(this.GetComponent<RectTransform>().anchoredPosition , this.Num);
        }
        else
        {
            skill.isClick = true;
            skill.start = this.transform.GetComponent<RectTransform>().anchoredPosition;
            skill.isConnect[Num] = 1;
        }
        PointLight();
    }
    void PointLight()
    {
        this.GetComponent<Image>().sprite = Resources.Load<Sprite>(@"Image/Strength/point_light");
    }
}
