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

    int size;

    public TextMeshProUGUI ClearText;

    bool isSuccess = false;

    void Start()
    {
        panel = GameObject.Find("Panel");
        _panelController = panel.GetComponent<PanelController>();

        Create_Image();
        Invoke("Finish" , 15f);
    }


    void Update()
    {
        if(isSuccess)
        {
            var obj = Instantiate(ClearText , new Vector3(0f , 0f , 0f) , Quaternion.identity);
            obj.transform.SetParent(panel.transform , false);
            obj.rectTransform.anchoredPosition = new Vector2(0f , 0f);

            Invoke("rateDamage" , 0.8f);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Finish();
        }
        Check_ImageColor();
    }
    void Create_Image()
    {
        int i = 0;
        TextAsset csv = Resources.Load(@"CSV/RateDamage") as TextAsset;
        StringReader st = new StringReader(csv.text);
        string[] info = st.ReadLine().Split(',');
        size = int.Parse(info[1]);
        images = new RateDamageImage[size];
        System.Random r = new System.Random();
        while(st.Peek() > -1)
        {
            string[] values = st.ReadLine().Split(',');
            var obj = Instantiate(panels[0] , new Vector3(0 , 0 , 0) , Quaternion.identity);

            obj.transform.SetParent(panel.transform , false);
            obj.rectTransform.anchoredPosition = new Vector2(float.Parse(values[3]) , float.Parse(values[4]));
            obj.GetComponent<RateDamageImage>().isCenter = int.Parse(values[1]);
            obj.GetComponent<RateDamageImage>().num = r.Next(0 , 4);
            images[i] = obj.GetComponent<RateDamageImage>();
            i++;
        }
    }
    void Finish()
    {

        //スキル使用時に遅くなった時間を戻す
        Time.timeScale = 1.0f;

        foreach (Transform n in panel.transform)
        {
            Destroy(n.gameObject);
        }
        Destroy(this.gameObject);
    }
    void Check_ImageColor()
    {
        int count = 0;
        for(int i = 0;i < size;i++)
        {
            if(i == size - 1)
            {
                if (images[i].num == images[0].num) count++;
            }
            else
            {
                if (images[i].num == images[i + 1].num) count++;
            }
        }
        if(count == size)
        {
            isSuccess = true;
        }
    }
    void rateDamage()
    {
        Finish();
    }
}
