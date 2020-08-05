using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RateDamage : MonoBehaviour
{
    public Image[] panels;

    RateDamageImage[] images;

    GameObject panel;

    PanelController _panelController;
    PlayerController _playerController;

    public string[] Names;

    int count;
    int size;
    int shape;
    int num;

    public TextMeshProUGUI ClearText;

    void Start()
    {
        panel = GameObject.Find("Panel");
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        Create_Image();
        Invoke("Finish" , 200f);
    }


    void Update()
    {
        if(count >= size)
        {
            var obj = Instantiate(ClearText , new Vector3(0f , 0f , 0f) , Quaternion.identity);
            obj.transform.SetParent(panel.transform , false);
            obj.rectTransform.anchoredPosition = new Vector2(0f , 0f);

            Invoke("_rateDamage" , 0.8f);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Finish();
        }
    }
    void Create_Image()
    {
        int i = 0;
        count = 1;
        TextAsset csv = Resources.Load(@"CSV/RateDamage/RateDamage") as TextAsset;
        StringReader st = new StringReader(csv.text);

        string[] info = st.ReadLine().Split(',');
        size = int.Parse(info[1]);
        images = new RateDamageImage[size];
        System.Random r = new System.Random();
        float prov = (float)Screen.height / 450;

        Names = st.ReadLine().Split(',');

        while(st.Peek() > -1)
        {
            string[] values = st.ReadLine().Split(',');
            var obj = Instantiate(panels[0] , new Vector3(0 , 0 , 0) , Quaternion.identity);

            obj.transform.SetParent(panel.transform , false);
            obj.rectTransform.anchoredPosition = new Vector2(float.Parse(values[3]) * prov , float.Parse(values[4]) * prov);
            obj.rectTransform.sizeDelta *= new Vector2(prov , prov);
            int center = int.Parse(values[1]);
            obj.GetComponent<RateDamageImage>().isCenter = center;
            obj.GetComponent<RateDamageImage>().num = r.Next(1 , 6);
            obj.GetComponent<RateDamageImage>().shape = r.Next(0 , 4);
            obj.GetComponent<RateDamageImage>()._rateDamage = this.gameObject.GetComponent<RateDamage>();
            images[i] = obj.GetComponent<RateDamageImage>();
            if(center == 1)
            {
                num = obj.GetComponent<RateDamageImage>().num;
                shape = obj.GetComponent<RateDamageImage>().shape;
            }
            i++;
        }
    }
    void Finish()
    {
        _panelController.isSkill = false;
        //スキル使用時に遅くなった時間を戻す
        Time.timeScale = 1.0f;

        foreach (Transform n in panel.transform)
        {
            Destroy(n.gameObject);
        }
        Destroy(this.gameObject);
    }
    public bool Check_Image(int shape,int num,bool boolen)
    {
        if(shape == this.shape && num == this.num && !boolen)
        {
            count++;
            return true;
        }
        else if(boolen)
        {
            count--;
        }
        return false;
    }
    void _rateDamage()
    {
        _playerController.RateDamage();
        Finish();
    }
}
