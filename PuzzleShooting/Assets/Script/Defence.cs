using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Defence : MonoBehaviour
{
    public Image[] peaces;
    public Image frame;
    public TextMeshProUGUI ClearText;
    public Text Explanation;
    public Image time;
    Image TimeImage;

    PanelController _panelController;

    GameObject panel;
    public Image select;

    float[] onPeace;
    float startTime;

    public int size;
    public int width;
    int height;

    public bool isSuccess;

    void Start()
    {
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
        panel = GameObject.Find("Panel");
        Create_peace();

        Invoke("Finish" , 30f);

        startTime = Time.time;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && select != null) select.GetComponent<DefenceImage>().Turn();

        if(isSuccess)
        {
            var obj = Instantiate(ClearText , panel.transform);
            obj.rectTransform.anchoredPosition = new Vector2(0f , -70f);

            Invoke("Succese" , 0.8f);
        }
        {
            float t = -360 * (Time.time - startTime) / 30f;
            TimeImage.rectTransform.rotation = Quaternion.Euler(0 , 0 , t);
        }
        CheckPease();
    }

    void Create_peace()
    {
        float prov = Screen.height / 450f;

        var text = Instantiate(Explanation , panel.transform);
        text.rectTransform.sizeDelta = new Vector2(prov , 90f * prov);
        text.rectTransform.anchoredPosition = new Vector2(0f , 160f * prov);
        text.fontSize = (int)(text.fontSize * prov);

        TimeImage = Instantiate(time , panel.transform);
        TimeImage.rectTransform.anchoredPosition = new Vector2(40 * prov , -180 * prov);
        TimeImage.rectTransform.sizeDelta *= prov;

        TextAsset explanation = Resources.Load(@"CSV/Defence/Explanation") as TextAsset;
        StringReader sr = new StringReader(explanation.text);
        text.text = "";

        while(sr.Peek() > -1)
        {
            string s = sr.ReadLine();
            text.text += s + "\n";
        }
        System.Random r = new System.Random();

        isSuccess = false;
        TextAsset csv = Resources.Load(@"CSV/Defence/Defence" + r.Next(1 , 3).ToString()) as TextAsset;
        StringReader st = new StringReader(csv.text);

        string[] info = st.ReadLine().Split(',');
        size = int.Parse(info[1]);
        width = int.Parse(info[2]);
        height = int.Parse(info[3]);
        onPeace = new float[size];

        while(st.Peek() > -1)
        {
            string[] values = st.ReadLine().Split(',');
            if(values[0] == "0")
            {
                var obj = Instantiate(frame , panel.transform);
                obj.rectTransform.anchoredPosition = new Vector2(float.Parse(values[1]) * prov , float.Parse(values[2]) * prov);
                obj.rectTransform.sizeDelta *= prov;
                obj.GetComponent<DefenceImage>().posNum = values[4];
                string[] Pos = values[4].Split('.');
                obj.GetComponent<DefenceImage>().index = int.Parse(Pos[0]) + int.Parse(Pos[1]) * width;
                obj.GetComponent<DefenceImage>()._defence = this.GetComponent<Defence>();
                obj.name = values[4];
            }
            else
            {
                var obj = Instantiate(peaces[int.Parse(values[0]) - 1] , panel.transform);
                int Asize = int.Parse(values[4]);
                obj.GetComponent<DefenceImage>().posNums = new string[Asize];
                for(int j = 0; j < Asize; j++) obj.GetComponent<DefenceImage>().posNums[j] = values[5 + j];

                obj.rectTransform.anchoredPosition = new Vector2(float.Parse(values[1]) * prov , float.Parse(values[2]) * prov);
                obj.rectTransform.sizeDelta *= prov;

                obj.GetComponent<DefenceImage>()._defence = this.GetComponent<Defence>();

                foreach(Transform p in obj.transform)
                {
                    p.gameObject.GetComponent<RectTransform>().anchoredPosition *= prov;
                    p.gameObject.GetComponent<RectTransform>().sizeDelta *= prov;
                    p.gameObject.GetComponent<DefenceImage>()._defence = this.GetComponent<Defence>();
                }
            }
        }
    }
    void Finish()
    {
        if(isSuccess) GameObject.Find("GameController").GetComponent<GameController>().IntervalSpawn(5 , _panelController.skillnum , 15f);
        else GameObject.Find("GameController").GetComponent<GameController>().IntervalSpawn(5 , _panelController.skillnum , 25f);

        _panelController.canskill[_panelController.skillnum] = false;
        _panelController.isSkill = true;

        _panelController.skillSpeed = 1;
        //スキル使用時に遅くなった時間を戻す
        Time.timeScale = 1.0f;

        foreach(Transform n in panel.transform)
        {
            Destroy(n.gameObject);
        }

        _panelController.Finish();
        panel.SetActive(false);
        _panelController.FinishTimeSet();
        Destroy(this.gameObject);
    }
    void Succese()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().Defence();
        Finish();
    }
    public void SetPeaceNum(int index)
    {
        if(!(index < 0 || index >= size)) onPeace[index] = 1;
    }
    public void ClearPeaceNum(int index)
    {
        if(!(index < 0 || index >= size)) onPeace[index] = 0;
    }
    void CheckPease()
    {
        int count = 0;
        foreach(var x in onPeace)
        {
            if(x == 1f) count++;
        }
        if(count == size) isSuccess = true;
    }
}
