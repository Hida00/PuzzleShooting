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
    public Text Setting;
    public Image image;

    void Start()
    {
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
