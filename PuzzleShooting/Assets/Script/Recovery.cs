using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;

public class Recovery : MonoBehaviour
{
    //Imageオブジェクト
    public Image[] panels;
    public Text Explanation;
    public Image time;
    Image TimeImage;

    RecoveryImage[] _imageObjects;

    //生成したImageオブジェクトを子に持つ
    GameObject panel;

    //PanelControllerオブジェクト、終了時に使用
    PanelController _panelController;

    //上・右・下・左の繋がる所は１、繋がらない所は０
    Dictionary<int , int[]> isLight = new Dictionary<int , int[]>();

    //プレイヤーの体力回復用
    PlayerController _playerController;

    public TextMeshProUGUI ClearText;

    float startTime;

    public int Filesize;

    public bool isSuccess = false;

    void Start()
    {
        //Panelの取得
        panel = GameObject.Find("Panel");
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        //パズルのImageオブジェクトを生成
        Create_Image();

        Invoke("Finish" , 15f);

        startTime = Time.time;
    }

    void Update()
    {
        if(isSuccess)
        {
            var obj = Instantiate(ClearText , new Vector3(0f , 0f , 0f) , Quaternion.identity);
            obj.transform.SetParent(panel.transform , false);
            obj.rectTransform.anchoredPosition = new Vector2(0f , -70f);

            Invoke("Succese" , 0.8f);
        }
        {
            float t = -360 * (Time.time - startTime) / 15f;
            TimeImage.rectTransform.rotation = Quaternion.Euler(0 , 0 ,t);
        }
        CheckLight();
    }

    void Create_Image()
    {
        int i = 0;
        System.Random r = new System.Random();
        int FileNum = r.Next(1 , Filesize);
        //CSVファイルから読み込み
        TextAsset csv = Resources.Load(@"CSV/Recovery/Recovery" + FileNum.ToString()) as TextAsset;
        StringReader sr = new StringReader(csv.text);

        float prov = (float)Screen.height / 450;
        string[] info = sr.ReadLine().Split(',');
        int size = int.Parse(info[0]);
        _imageObjects = new RecoveryImage[size];

        //オブジェクトの種類、x座標、y座標、z回転
        while (sr.Peek() > -1)
        {
            string[] values = sr.ReadLine().Split(',');
            var obj = Instantiate(panels[int.Parse(values[0])] , new Vector3(0 , 0 , 0) , Quaternion.identity);
            obj.transform.SetParent(panel.transform , false);
            obj.rectTransform.anchoredPosition = new Vector2(float.Parse(values[1]) * prov , float.Parse(values[2]) * prov);
            obj.rectTransform.sizeDelta *= new Vector2(prov , prov);

            Quaternion q = Quaternion.Euler(0 , 0 , float.Parse(values[3]));
            obj.transform.rotation *= q;

            isLight.Add(i , new int[4] { int.Parse(values[5]) , int.Parse(values[6]) , int.Parse(values[7]) , int.Parse(values[8]) });
            _imageObjects[i] = obj.GetComponent<RecoveryImage>();

            obj.GetComponent<RecoveryImage>().originalNum = i;
            i++;
        }

        var text = Instantiate(Explanation , panel.transform);
        text.rectTransform.sizeDelta = new Vector2(prov , 90f * prov);
        text.rectTransform.anchoredPosition = new Vector2(0f , 160f * prov);
        text.fontSize = (int)(text.fontSize * prov);

        TimeImage = Instantiate(time , panel.transform);
        TimeImage.rectTransform.anchoredPosition = new Vector2(40 * prov , -180 * prov);
        TimeImage.rectTransform.sizeDelta *= prov;

        TextAsset explanation = Resources.Load(@"CSV/Recovery/Explanation") as TextAsset;
        StringReader st = new StringReader(explanation.text);
        text.text = "";
        while(st.Peek() > -1)
        {
            string s = st.ReadLine();
            text.text += s;
            text.text += "\n";
        }
    }

    void CheckLight()
    {
        int sq_size = (int)Math.Sqrt(isLight.Count);
        for (int j = 0; j < isLight.Count; j++)
        {
            if (j >= sq_size)
            {
                int buf = isLight[j][0];
                int buf2 = isLight[j - sq_size][2];
                if (buf == buf2 && buf2 == 1)
                {
                    if (_imageObjects[j - sq_size].islight == 1)
                    {
                        _imageObjects[j].islight = 1;
                    }
                    if (_imageObjects[j].islight == 1)
                    {
                        _imageObjects[j - sq_size].islight = 1;
                    }
                }
            }
            if ((j + 1) % 3 != 0)
            {
                int buf = isLight[j][1];
                int buf2 = isLight[j + 1][3];
                if (buf == buf2 && buf2 == 1)
                {
                    if (_imageObjects[j + 1].islight == 1)
                    {
                        _imageObjects[j].islight = 1;
                    }
                    if (_imageObjects[j].islight == 1)
                    {
                        _imageObjects[j + 1].islight = 1;
                    }
                }
            }

        }
    }
    public void TurnImage(int Num)
    {
        int buf = isLight[Num][0];
        isLight[Num][0] = isLight[Num][3];
        isLight[Num][3] = isLight[Num][2];
        isLight[Num][2] = isLight[Num][1];
        isLight[Num][1] = buf;
        _imageObjects[Num].islight = 0;
    }
    public void Succese()
    {
        float value = _playerController.maxHealth * 0.15f;
        _playerController.health_Point += value;
        Finish();
    }
    void Finish()
    {
        if(isSuccess)
        {
            GameObject.Find("GameController").GetComponent<GameController>().IntervalSpawn(0 , _panelController.skillnum , 15f);
        }
        else
        {
            GameObject.Find("GameController").GetComponent<GameController>().IntervalSpawn(0 , _panelController.skillnum , 25f);
        }

        _panelController.canskill[_panelController.skillnum] = false;
        _panelController.isSkill = true;

        //スキル使用時に遅くなった時間を戻す
        _panelController.skillSpeed = 1;
        Time.timeScale = 1.0f;

        foreach (Transform n in panel.transform)
        {
            Destroy(n.gameObject);
        }

        _panelController.Finish();
        panel.SetActive(false);
        _panelController.FinishTimeSet();
        Destroy(this.gameObject);
    }
}
