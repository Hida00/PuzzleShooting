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

    public Text Setting;
    public Text SkillKey;
    public TextMeshProUGUI Exit;

    public Text SkillSelect;
    public Text JumpSkillSelect;

    public Text volumeText;
    public Text valueText;
    public Slider volumeSlider;

    public Text MuteText;
    public Toggle MuteToggle;

    public Text[] Skill;

    public AudioSource BGM;

    int num;

    bool isFunc = false;
    bool Mute = false;

    public static KeyCode[] skill = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };
    KeyCode[] buf;

    void Start()
    {
        volumeSlider.value = (int)(SelectController.volume * 10f + 0.5f);
        valueText.text = volumeSlider.value.ToString();
        buf = skill;

        BGM.volume = SelectController.volume;
        BGM.loop = true;
        BGM.Play();
    }


    void Update()
    {
        volumeSlider.value = (int)(volumeSlider.value + 0.5f);
        valueText.text = volumeSlider.value.ToString();

        ClickMute();

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
        num = 0;
    }
    public void Skill2_KeySet()
    {
        isFunc = true;
        num = 1;
    }
    public void Skill3_KeySet()
    {
        isFunc = true;
        num = 2;
    }
    void SkillKey_Set(int num)
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code) && code != KeyCode.Mouse0 && code != KeyCode.Mouse1)
                {
                    buf[num] = code;
                    isFunc = false;
                    break;
                }
                else if(code == KeyCode.Mouse0 || code == KeyCode.Mouse1)
                {
                    isFunc = false;
                    break;
                }
            }
        }
    }

    public void Exit_Click()
    {
        skill = buf;
        PanelController.buf_keys = skill;
        SelectController.volume = volumeSlider.value / 10f;
        if(Mute) SelectController.volume = 0f;
        
        BGM.Stop();
        SceneManager.LoadScene("Select");
    }
    public void ClickMute()
    {
        if(MuteToggle.GetComponent<Toggle>().isOn) Mute = true;
        else Mute = false;
    }
    public void SetSkill()
    {
        SceneManager.LoadScene("SkillSelect");
    }
    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }
}
