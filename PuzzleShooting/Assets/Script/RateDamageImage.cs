using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateDamageImage : MonoBehaviour
{
    public RateDamage _rateDamage;

    [NonSerialized]
    public int num;
    public int shape;
    public int isCenter;

    bool ImageSet = false;

    void Start()
    {
        Change_Image();
    }

    void Update()
    {
        if(isCenter == 0) Change_Image();
    }
    public void Image_Click()
    {
        if(isCenter == 0 && Input.GetMouseButtonUp(0))
        {
            num++;
            if(num == 6)
            {
                shape++;
                num = 1;
                if(shape == 4) shape = 0;
            }
        }
        if(isCenter == 0 && Input.GetMouseButtonUp(1))
        {
            num--;
            if(num == 0)
            {
                shape--;
                num = 5;
                if(shape == -1) shape = 3;
            }
        }
        ImageSet = _rateDamage.Check_Image(shape , num , ImageSet);
    }
    void Change_Image()
    {
        string Shape = _rateDamage.Names[shape];
        Sprite sprite = Resources.Load<Sprite>(@"Image/RateDamage/" + Shape + num.ToString());
        this.GetComponent<Image>().sprite = sprite;
    }
}
