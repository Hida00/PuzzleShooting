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

    public float difTime;

    //スキル識別用の値
    int Skill1_num = 3;
    int Skill2_num = 4;
    int Skill3_num = 2;
    public int skillSpeed = 1;

    [NonSerialized]
    public bool isSkill = false;

    void Start()
    {
        var area = Panel.transform.parent.gameObject.GetComponent<RectTransform>();
        Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(area.sizeDelta.x * 0.5f , area.sizeDelta.y);
        Panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-Panel.GetComponent<RectTransform>().sizeDelta.x * 0.5f , 0);
        SetSkillNumber();

        //Panelのゲームオブジェクトを格納
        Panel = GameObject.Find("Panel");

        //Panelの色を白に変更
        Panel.GetComponent<Image>().color = Color.clear;

        //Panelを無効化、スキルキー入力で有効
        Panel.SetActive(false);

        Skill_Keys = buf_keys;
        skillSpeed = 1;
    }

    void Update()
    {
        //スキルキー入力を取得、Panelの有効化
        if (Input.GetKeyDown(Skill_Keys[0]) && !isSkill)
        {
            Panel.SetActive(true);

            EnemyTimeSet();
            Instantiate(Skills[Skill1_num] , new Vector3(0 , 0 , 0) , Quaternion.identity);
            //スキル1の処理はここに書く
        }
        else if (Input.GetKeyDown(Skill_Keys[1]) && !isSkill)
        {
            Panel.SetActive(true);

            EnemyTimeSet();
            Instantiate(Skills[Skill2_num] , new Vector3(0 , 0 , 0) , Quaternion.identity);
            //スキル2の処理はここに書く
        }
        else if (Input.GetKeyDown(Skill_Keys[2]) && !isSkill)
        {
            Panel.SetActive(true);

            EnemyTimeSet();
            Instantiate(Skills[Skill3_num] , new Vector3(0 , 0 , 0) , Quaternion.identity);
            //スキル3の処理はここに書く
        }
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
    void SetSkillNumber()
    {
        Skill1_num = SelectController.SetSkills[0];
        Skill2_num = SelectController.SetSkills[1];
        Skill3_num = SelectController.SetSkills[2];
    }
    public void FinishTimeSet()
    {
        EnemyTimeSet();
        GameObject.Find("Generator").GetComponent<Generator>().startTime = Time.time - difTime;
    }
    void EnemyTimeSet()
    {
        if(isSkill)
        {
            var Enemy = GameObject.FindGameObjectsWithTag("ENEMY");

            foreach(var obj in Enemy)
            {
                obj.GetComponent<Viran>().startTime = Time.time - obj.GetComponent<Viran>().difTime;
            }
            var Boss = GameObject.FindGameObjectsWithTag("BOSS");

            if(Boss.Length != 0)
                Boss[0].GetComponent<Boss>().StartTime = Time.time - Boss[0].GetComponent<Boss>().difTime;

            isSkill = false;
        }
        else
        {
            var Enemy = GameObject.FindGameObjectsWithTag("ENEMY");

            foreach(var obj in Enemy)
            {
                obj.GetComponent<Viran>().difTime = Time.time - obj.GetComponent<Viran>().startTime;
            }
            var Boss = GameObject.FindGameObjectsWithTag("BOSS");

            if(Boss.Length != 0)
                Boss[0].GetComponent<Boss>().difTime = Time.time - Boss[0].GetComponent<Boss>().StartTime;

            Time.timeScale = 1f;
            skillSpeed = 0;
            isSkill = true;
            difTime = Time.time - GameObject.Find("Generator").GetComponent<Generator>().startTime;
        }
    }
}
