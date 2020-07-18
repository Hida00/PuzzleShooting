using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateDamageImage : MonoBehaviour
{
    [NonSerialized]
    public int num;
    public int isCenter;

    void Start()
    {
        this.GetComponent<Image>().sprite = Resources.Load<Sprite>(@"Image/RateDamage/image" + num.ToString());
    }

    void Update()
    {
        
    }
    public void Image_Click()
    {
        if(isCenter == 0)
        {
            num++;
            if (num == 4) num = 0;

            Sprite sprite = Resources.Load<Sprite>(@"Image/RateDamage/image" + num.ToString());
            this.GetComponent<Image>().sprite = sprite;
        }
    }
}
