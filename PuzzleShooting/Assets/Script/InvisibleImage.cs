using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvisibleImage : MonoBehaviour
{
    Invisible _invisible;

    public TextMeshProUGUI NumberText;

    public int index;
    public int Num;

    void Start()
    {
        _invisible = GameObject.Find("Invisible(Clone)").GetComponent<Invisible>();
    }


    void Update()
    {
        if (Num != 0)
        {
            NumberText.text = Num.ToString();
        }
        else
        {
            NumberText.text = "";
        }
    }
    public void Image_Click()
    {
        Num = _invisible.ChangeNum(index , Num);
    }
}
