using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillFrame : MonoBehaviour
{
    SkillSelect _skillSelect;

    public Image set_Image;

    public int frameNum = 0;

    void Start()
    {
        _skillSelect = GameObject.Find("SkillSelect").GetComponent<SkillSelect>();

        Set_Frame();
    }

    void Update()
    {

    }
    void Set_Frame()
    {
        float width = -Screen.width * 0.25f;
        float height = this.GetComponent<RectTransform>().sizeDelta.y * 1.02f;
        if(frameNum == 2)
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(width , 0);
        }
        else if(frameNum == 1)
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(width , height);
        }
        else if(frameNum == 3)
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(width , -height);
        }
    }
    public void ClickReset()
    {
        set_Image.transform.position = set_Image.GetComponent<SkillPanels>().StartPos;

    }
    public void SetImage(Image image)
    {
        if(set_Image != null)
        {
            set_Image.GetComponent<SkillPanels>().Parent.select = false;
            Destroy(set_Image.gameObject);
        }
        set_Image = image;
        _skillSelect.Numbers[frameNum - 1] =  image.GetComponent<SkillPanels>().Num;
    }
}
