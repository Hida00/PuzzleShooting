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
        //Debug用。Escapeキーを押すとスキルの終了
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _panelController.isSkill = false;
        }
    }
    public void Image_Click()
    {
        if(skill.isClick)
        {
            skill.DrawLine(this.transform.localPosition , this.Num);
        }
        else
        {
            skill.isClick = true;
            skill.start = this.transform.localPosition;
            skill.isConnect[Num] = 1;
        }
        var sprite = Resources.Load<Sprite>(@"Image/Strength/point_light");
        this.GetComponent<Image>().sprite = sprite;
    }
    public void Image_Move()
    {
        skill.start = this.transform.localPosition;
        skill.buf = this.Num;
    }
    public void Image_Enter()
    {
        skill.DrawLine(this.transform.localPosition,this.Num);
    }
}
