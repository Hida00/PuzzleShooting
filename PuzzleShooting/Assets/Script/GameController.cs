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
    public GameObject LeftArea;
    public GameObject RightArea;
    Slider bossHP;


    public TextMeshProUGUI scoreText;

    public int _score = 0;

    void Start()
    {
        Application.targetFrameRate = 65;

        bossHP = GameObject.Find("bossHP").GetComponent<Slider>();
        bossHP.gameObject.SetActive(false);

        float prov = (float)Screen.height / 450;
        LeftArea.GetComponent<RectTransform>().sizeDelta *= prov;
        LeftArea.GetComponent<RectTransform>().anchoredPosition *= prov;
        RightArea.GetComponent<RectTransform>().sizeDelta *= prov;
        RightArea.GetComponent<RectTransform>().anchoredPosition *= prov;

        scoreText.rectTransform.anchoredPosition *= prov;
        scoreText.rectTransform.anchoredPosition -= new Vector2(10f , 5f);
        scoreText.rectTransform.sizeDelta *= prov;
        scoreText.fontSize *= (prov * 3f / 4f);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace)) Quit();

        scoreText.text = "Score:" + _score.ToString();
    }
    void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            Application.Quit();
        #endif
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
}
