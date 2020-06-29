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

    //生成したImageオブジェクトを子に持つ
    GameObject panel;

    //PanelControllerオブジェクト、終了時に使用
    PanelController _panelController;

    void Start()
    {
        //Panelの取得
        panel = GameObject.Find("Panel");
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();

        //パズルのImageオブジェクトを生成
        Create_Image(panels.Length);
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
    }

    void Create_Image(int length)
    {
        //CSVファイルから読み込み
        TextAsset csv = Resources.Load(@"CSV/RecoveryCSV") as TextAsset;
        StringReader sr = new StringReader(csv.text);
        //オブジェクトの種類、x座標、y座標、z回転
        while(sr.Peek() > -1)
        {
            string[] values = sr.ReadLine().Split(',');
            var obj = Instantiate(panels[int.Parse(values[0])] , new Vector3(0 , 0 , 0) , Quaternion.identity);
            obj.transform.SetParent(panel.transform , false);
            obj.rectTransform.anchoredPosition = new Vector2(float.Parse(values[1]) , float.Parse(values[2]));
            Quaternion q = Quaternion.Euler(0 , 0 , float.Parse(values[3]));
            obj.transform.rotation *= q;
        }
    }
}
