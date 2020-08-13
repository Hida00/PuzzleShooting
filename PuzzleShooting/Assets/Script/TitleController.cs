using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleController : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public Text Play;
    public Text Quit;
    
    void Start()
    {
        float prov = (float)Screen.height / 450;
        Title.rectTransform.sizeDelta *= prov;
        Play.rectTransform.sizeDelta *= prov;
        Quit.rectTransform.sizeDelta *= prov;
        Title.rectTransform.anchoredPosition *= prov;
        Play.rectTransform.anchoredPosition *= prov;
        Quit.rectTransform.anchoredPosition *= prov;
        Title.fontSize *= prov;
        Play.fontSize = (int)(Play.fontSize * prov);
        Quit.fontSize = (int)(Quit.fontSize * prov);
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
