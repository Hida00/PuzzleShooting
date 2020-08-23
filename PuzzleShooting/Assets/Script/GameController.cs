using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    public GameObject canvas;
    public GameObject LeftArea;
    public GameObject RightArea;
    public GameObject DataArea;
    public GameObject[] SkillInterval;
    public Image Pause;
    public GameObject PausePanel;
    Slider bossHP;
    public AudioSource BGM;

    PanelController _panelController;

    public TextMeshProUGUI scoreText;

    public float[] intervalTimes;

    public int _score = 0;

    bool[] boolen = new bool[3];
    bool isPause = false;
    bool isSkill = false;

    void Start()
    {
        Application.targetFrameRate = 65;

        float scale = (Screen.height / 20f) * (40f / Screen.width);
        GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize *= scale;

        bossHP = GameObject.Find("bossHP").GetComponent<Slider>();
        bossHP.gameObject.SetActive(false);

        float prov = (float)Screen.width / 928;
        LeftArea.GetComponent<RectTransform>().sizeDelta *= prov;
        LeftArea.GetComponent<RectTransform>().anchoredPosition *= prov;
        RightArea.GetComponent<RectTransform>().sizeDelta *= prov;
        RightArea.GetComponent<RectTransform>().anchoredPosition *= prov;

        scoreText.rectTransform.anchoredPosition *= prov;
        scoreText.rectTransform.anchoredPosition -= new Vector2(10f , 5f);
        scoreText.rectTransform.sizeDelta *= prov;
        scoreText.fontSize *= (prov * 3f / 4f);

        canvas.GetComponent<Image>().sprite = Resources.Load<Sprite>(@"Image/other/" + SelectController.StageImage);
        canvas.GetComponent<Image>().color = new Color(1f , 1f , 1f , 0.7f);

        DataArea.GetComponent<RectTransform>().sizeDelta =
            new Vector2(LeftArea.GetComponent<RectTransform>().sizeDelta.x / 2f , LeftArea.GetComponent<RectTransform>().sizeDelta.y);
        DataArea.GetComponent<RectTransform>().anchoredPosition *= prov;

        foreach(var x in SkillInterval)
        {
            x.SetActive(false);

            x.GetComponent<RectTransform>().anchoredPosition *= prov;
            x.GetComponent<RectTransform>().sizeDelta *= prov;

            var img = x.transform.GetChild(0).GetComponent<Image>();
            img.rectTransform.anchoredPosition *= prov;
            img.rectTransform.sizeDelta *= prov;

            var Text = x.transform.GetChild(1).GetComponent<Text>();
            Text.rectTransform.anchoredPosition *= prov;
            Text.rectTransform.sizeDelta *= prov;
            Text.fontSize = (int)(Text.fontSize * prov);
        }

        Pause.rectTransform.anchoredPosition *= prov;
        Pause.rectTransform.sizeDelta *= prov;
        PausePanel.GetComponent<Image>().color = new Color(0.375f , 0.375f , 0.375f , 0.5f);
        PausePanel.SetActive(false);
        
        var child = PausePanel.transform.GetChild(0).GetComponent<RectTransform>();
        child.anchoredPosition = Vector2.zero * prov;
        child.sizeDelta *= prov;
        child.GetComponent<Image>().color = new Color(0.75f , 0.75f , 0.75f , 0.75f);

        var Child = child.transform.GetChild(0).GetComponent<RectTransform>();
        Child.anchoredPosition *= prov;
        Child.sizeDelta *= prov;
        Child.GetComponent<Text>().fontSize = (int)(Child.GetComponent<Text>().fontSize * (1 + (prov - 1) / 2));

        Child = child.transform.GetChild(1).GetComponent<RectTransform>();
        Child.anchoredPosition *= prov;
        Child.sizeDelta *= prov;
        Child.transform.GetChild(0).GetComponent<Text>().fontSize = (int)(Child.transform.GetChild(0).GetComponent<Text>().fontSize * prov);

        Child = child.transform.GetChild(2).GetComponent<RectTransform>();
        Child.anchoredPosition *= prov;
        Child.sizeDelta *= prov;
        Child.transform.GetChild(0).GetComponent<Text>().fontSize = (int)(Child.transform.GetChild(0).GetComponent<Text>().fontSize * prov);

        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
        intervalTimes = new float[3] { 20 , 0 , 0 };

        BGM.clip = Resources.Load<AudioClip>(@"Music/" + SelectController.MusicName);
        BGM.volume = SelectController.volume;
        BGM.loop = true;
        BGM.Play();
    }

    void Update()
    {
        if(boolen[0])
        {
            float prov = Screen.height / 450f;
            var obj = SkillInterval[0].transform.GetChild(1).GetComponent<Text>();
            intervalTimes[0] -= Time.deltaTime;
            if(intervalTimes[0] < 10f && intervalTimes[0] >= 0) obj.text = "0:0" + (int)intervalTimes[0];
            else if(intervalTimes[0] >= 0) obj.text = "0:" + (int)intervalTimes[0];
            else
            {
                _panelController.canskill[0] = true;
                SkillInterval[0].SetActive(false);
                boolen[0] = false;
            }
        }
        if(boolen[1])
        {
            var obj = SkillInterval[1].transform.GetChild(1).GetComponent<Text>();
            intervalTimes[1] -= Time.deltaTime;
            if(intervalTimes[1] < 10f && intervalTimes[1] >= 0) obj.text = "0:0" + (int)intervalTimes[1];
            else if(intervalTimes[1] >= 0) obj.text = "0:" + (int)intervalTimes[1];
            else
            {
                _panelController.canskill[1] = true;
                SkillInterval[1].SetActive(false);
                boolen[1] = false;
            }
        }
        if(boolen[2])
        {
            var obj = SkillInterval[2].transform.GetChild(1).GetComponent<Text>();
            intervalTimes[2] -= Time.deltaTime;
            if(intervalTimes[2] < 10f && intervalTimes[2] >= 0) obj.text = "0:0" + (int)intervalTimes[2];
            else if(intervalTimes[2] >= 0) obj.text = "0:" + (int)intervalTimes[2];
            else
            {
                _panelController.canskill[2] = true;
                SkillInterval[2].SetActive(false);
                boolen[2] = false;
            }
        }

        scoreText.text = "Score:" + _score.ToString();
    }
    public void Quit()
    {
        SceneManager.LoadScene("Select");
    }
    public void Clear()
    {
        SceneManager.LoadScene("Result");
    }
    public void BossBulletMoveStart()
    {
        var Enemy = GameObject.FindGameObjectsWithTag("BULLET");

        foreach(var obj in Enemy)
        {
            obj.GetComponent<BulletController>().isBoss = false;
        }
    }
    public void FinishGame(bool isClear)
    {
        ResultController.score = _score;
        if(isClear)
        {
            ResultController.isClear = true;
        }
        else
        {
            ResultController.isClear = false;
        }
    }
    public void IntervalSpawn(int imageNum,int Num,float time)
    {
        if(!boolen[Num])
        {
            intervalTimes[Num] = time;

            SkillInterval[Num].SetActive(true);

            var img = SkillInterval[Num].transform.GetChild(0).GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>(@"Image/Skills/skill" + imageNum.ToString());

            var Text = SkillInterval[Num].transform.GetChild(1).GetComponent<Text>();
            Text.text = "0:" + intervalTimes[0];

            boolen[Num] = true;
        }
    }
    public void PauseClick()
    {
        if(isPause)
        {
            if(!isSkill) _panelController.isSkill = false;
            Time.timeScale = 1;
            isPause = false;
            PausePanel.SetActive(false);
        }
        else
        {
            if(_panelController.isSkill) isSkill = true;
            Time.timeScale = 0;
            _panelController.isSkill = true;
            isPause = true;
            PausePanel.SetActive(true);
        }
    }
    public void Title()
    {
        SceneManager.LoadScene("Title");
    }
}
