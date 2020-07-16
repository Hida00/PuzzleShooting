using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading;
using UnityEngine.SceneManagement;

public class SettingController : MonoBehaviour
{
    public GameObject Camera;
    public GameObject setting;

    public TextMeshProUGUI Setting;
    public TextMeshProUGUI SkillKey;
    public TextMeshProUGUI Save;
    public TextMeshProUGUI Exit;

    public TextMeshProUGUI[] Skill;

    int num;

    bool isFunc = false;

    public static KeyCode[] skill = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };
    KeyCode[] buf;

    void Start()
    {
        buf = skill;
        Save.enabled = false;
        Exit.enabled = true;
    }


    void Update()
    {
        if (!isFunc)
        {
            Skill[0].text = "Skill1:" + buf[0].ToString();
            Skill[1].text = "Skill2:" + buf[1].ToString();
            Skill[2].text = "Skill3:" + buf[2].ToString();
        }

        if (isFunc)
        {
            SkillKey_Set(num);
            Skill[num].text = "Key Input";
        }
    }

    public void Skill1_KeySet()
    {
        isFunc = true;
        Save.enabled = true;
        Exit.enabled = false;
        num = 0;
    }
    public void Skill2_KeySet()
    {
        isFunc = true;
        Save.enabled = true;
        Exit.enabled = false;
        num = 1;
    }
    public void Skill3_KeySet()
    {
        isFunc = true;
        Save.enabled = true;
        Exit.enabled = false;
        num = 2;
    }
    void SkillKey_Set(int num)
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    buf[num] = code;
                    isFunc = false;
                    break;
                }
            }
        }
    }

    public void Save_Click()
    {
        Save.enabled = false;
        Exit.enabled = true;

        skill = buf;
    }

    public void Exit_Click()
    {
        PanelController.buf_keys = skill;
        SceneManager.LoadScene("Select");
    }
}
