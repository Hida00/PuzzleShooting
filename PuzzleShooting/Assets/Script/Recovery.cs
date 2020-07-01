using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using System.Threading;

public class Recovery : MonoBehaviour
{
    //Imageオブジェクトたち
    public Image[] panels;

    ImageScript[] _imageObjects;

    //生成したImageオブジェクトを子に持つ
    GameObject panel;

    //PanelControllerオブジェクト、終了時に使用
    PanelController _panelController;

    //上・右・下・左の繋がる所は１、繋がらない所は０
    Dictionary<int , int[]> isLight = new Dictionary<int , int[]>();

    int size;

    void Start()
    {
        //Panelの取得
        panel = GameObject.Find("Panel");
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();

        //パズルのImageオブジェクトを生成
        Create_Image();
    }

    void Update()
    {
        //終了時の処理
        if(!_panelController.isSkill)
        {
            Destroy(this.gameObject);
            panel.SetActive(false);
            //スキル使用時に遅くなった時間を戻す
            Time.timeScale = 1.0f;
        }
        CheckLight();
    }

    void Create_Image()
    {
        int i = 0;
        //CSVファイルから読み込み
        TextAsset csv = Resources.Load(@"CSV/RecoveryCSV") as TextAsset;
        StringReader sr = new StringReader(csv.text);
        string[] info = sr.ReadLine().Split(',');
        size = int.Parse(info[0]);
        _imageObjects = new ImageScript[size];
        //オブジェクトの種類、x座標、y座標、z回転
        while(sr.Peek() > -1)
        {
            string[] values = sr.ReadLine().Split(',');
            var obj = Instantiate(panels[int.Parse(values[0])] , new Vector3(0 , 0 , 0) , Quaternion.identity);
            obj.transform.SetParent(panel.transform , false);
            obj.rectTransform.anchoredPosition = new Vector2(float.Parse(values[1]) , float.Parse(values[2]));

            Quaternion q = Quaternion.Euler(0 , 0 , float.Parse(values[3]));
            obj.transform.rotation *= q;

            isLight.Add(i,new int[4] { int.Parse(values[5]) , int.Parse(values[6]) , int.Parse(values[7]) , int.Parse(values[8]) });
            _imageObjects[i] = obj.GetComponent<ImageScript>();

            obj.GetComponent<ImageScript>().originalNum = i;
            i++;
        }
    }
    
    void CheckLight()
    {
        int sq_size = (int)Math.Sqrt(isLight.Count);
        for(int j = 0;j < isLight.Count;j++)
        {
            if(j >= sq_size)
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
            if((j + 1) % 3 != 0)
            {
                int buf = isLight[j][1];
                int buf2 = isLight[j + 1][3];
                if(buf == buf2 && buf2 == 1)
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
}
