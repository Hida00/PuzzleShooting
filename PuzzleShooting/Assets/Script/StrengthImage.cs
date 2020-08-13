using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _panelController.isSkill = false;
        }
    }
    public void Image_Click()
    {
        if(skill.isClick)
        {
            Debug.Log(skill.isClick);
            skill.DrawLine(this.GetComponent<RectTransform>().anchoredPosition , this.Num);
        }
        else
        {
            skill.isClick = true;
            skill.start = this.transform.GetComponent<RectTransform>().anchoredPosition;
            skill.isConnect[Num] = 1;
        }
        skill.PointLight(this.GetComponent<RectTransform>());
    }
}
