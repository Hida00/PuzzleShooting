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
    }

    void Update()
    {

    }
    public void ClickReset()
    {

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
    public void EnterPointer() => _skillSelect.ShowExplanation(this.GetComponent<Text>().text);

    public void ExitPointer() => _skillSelect.DeleteExplanation(this.GetComponent<Text>().text);
}
