using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleController : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI TitleU;
    public Text Play;
    public Text PlayU;
    public Text Quit;
    public Text QuitU;
    public Text Setting;
    public Text SettingU;
    
    void Start()
    {
        float prov = (float)Screen.height / 450;

        Title.rectTransform.sizeDelta *= prov;
        Play.rectTransform.sizeDelta *= prov;
        Quit.rectTransform.sizeDelta *= prov;
        Setting.rectTransform.sizeDelta *= prov;
        TitleU.rectTransform.sizeDelta *= prov;
        PlayU.rectTransform.sizeDelta *= prov;
        QuitU.rectTransform.sizeDelta *= prov;
        SettingU.rectTransform.sizeDelta *= prov;

        Title.rectTransform.anchoredPosition *= prov;
        Play.rectTransform.anchoredPosition *= prov;
        Quit.rectTransform.anchoredPosition *= prov;
        Setting.rectTransform.anchoredPosition *= prov;
        TitleU.rectTransform.anchoredPosition *= prov;
        PlayU.rectTransform.anchoredPosition *= prov;
        QuitU.rectTransform.anchoredPosition *= prov;
        SettingU.rectTransform.anchoredPosition *= prov;

        Title.fontSize *= prov;
        TitleU.fontSize *= prov;

        Play.fontSize = (int)(Play.fontSize * prov);
        Quit.fontSize = (int)(Quit.fontSize * prov);
        Setting.fontSize = (int)(Setting.fontSize * prov);
        PlayU.fontSize = (int)(PlayU.fontSize * prov);
        QuitU.fontSize = (int)(QuitU.fontSize * prov);
        SettingU.fontSize = (int)(SettingU.fontSize * prov);
    }

    void Update()
    {

    }
    public void ClickPlay()
    {
        SceneManager.LoadScene("Select");
    }
    public void ClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
            Application.Quit();
#endif
    }
    public void ClickSetting()
    {
        SceneManager.LoadScene("Setting");
    }
    public void ClickStaff()
    {

    }
}
