using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    //スキルキー用のKeyCode
    KeyCode[] Skill_Keys;
    public static KeyCode[] buf_keys = { KeyCode.Alpha1 , KeyCode.Alpha2 , KeyCode.Alpha3 };

    //PanelObject
    public GameObject Panel;
    public GameObject[] Skills;

    //スキル識別用の値、仮に０と１と２とする
    int Skill1_num = 4;
    int Skill2_num = 1;
    int Skill3_num = 2;

    [NonSerialized]
    public bool isSkill = false;

    void Start()
    {
        //Panelのゲームオブジェクトを格納
        Panel = GameObject.Find("Panel");

        //Panelの色を白に変更
        Panel.GetComponent<Image>().color = Color.white;

        //スキルのキー割り当て、設定画面作ったら消す
        //デフォルトで１～３キー(テンキーではない方)

        //Panelを無効化、スキルキー入力で有効
        Panel.SetActive(false);

        Skill_Keys = buf_keys;
    }

    void Update()
    {
        //ウィンドウの横幅を取得,Panelの位置や大きさがウィンドウサイズに依存するようにする
        float Width = (float)Screen.width;
        float width = 0.32f * (float)Screen.width;
        float height = 0.40f * (float)Screen.height;

        //スキルキー入力を取得、Panelの有効化
        if (Input.GetKeyDown(Skill_Keys[0]) && !isSkill)
        {
            Panel.SetActive(true);
            //時間を遅くする
            Time.timeScale = 0.5f;
            isSkill = true;
            Instantiate(Skills[Skill1_num] , new Vector3(0 , 0 , 0) , Quaternion.identity);
            //スキル1の処理はここに書く
        }
        else if (Input.GetKeyDown(Skill_Keys[1]) && !isSkill)
        {
            Panel.SetActive(true);
            //時間を遅くする
            Time.timeScale = 0.5f;
            isSkill = true;
            Instantiate(Skills[Skill2_num] , new Vector3(0 , 0 , 0) , Quaternion.identity);
            //スキル2の処理はここに書く
        }
        else if (Input.GetKeyDown(Skill_Keys[2]) && !isSkill)
        {
            Panel.SetActive(true);
            //時間を遅くする
            Time.timeScale = 0.5f;
            isSkill = true;
            Instantiate(Skills[Skill3_num] , new Vector3(0 , 0 , 0) , Quaternion.identity);
            //スキル3の処理はここに書く
        }

        //   ※※変更※※
        //ここよくわかってない
        //ウィンドウサイズに依存してPanelのサイズが変わるようにする
        Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(width , height);
        Panel.GetComponent<RectTransform>().position = new Vector3(Width - (width / 2) - 20 , (height / 2) + 20 , 0);
        Panel.GetComponent<Image>().color = new Color(0.8f , 0.8f , 0.8f , 0.0f);
    }
    public void PointerEnter()
    {
        //カーソルを合わせるとPanelの背景を灰色にする
        //見にくかったりしたら変更可
        Panel.GetComponent<Image>().color = new Color(0.8f , 0.8f , 0.8f , 0.6f);
    }
    public void PointerExit()
    {
        //カーソルを外すとPanelの背景っを白にする
        //見にくかっt(ry
        Panel.GetComponent<Image>().color = new Color(1f , 1f , 1f , 0.6f);
    }
}
