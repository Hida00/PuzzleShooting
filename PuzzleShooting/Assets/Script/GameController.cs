using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject PlayArea;
    Slider bossHP;

    string fileName;

    public TextMeshProUGUI scoreText;

    public int _score = 0;

    void Start()
    {
        Application.targetFrameRate = 65;

        bossHP = GameObject.Find("bossHP").GetComponent<Slider>();
        bossHP.gameObject.SetActive(false);

        float prov = (float)Screen.height / 450;
        scoreText.rectTransform.anchoredPosition *= prov;
        PlayArea.GetComponent<RectTransform>().sizeDelta *= prov;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace)) Quit();

        scoreText.text = "Score:" + _score.ToString();
    }
    public void Set()
    {
        fileName = SelectController.SelectName;
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
}
