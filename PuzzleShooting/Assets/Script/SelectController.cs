using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour
{
    public static string SelectName = "Normal1";
    public static int[] SetSkills = { 0 , 1 , 2 };

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) Quit();
    }
    public void Click_One()
    {
        SelectName = "Normal1";
        SceneManager.LoadScene("PlayScene");
    }
    public void Click_Setting()
    {
        SceneManager.LoadScene("Setting");
    }
    public void Click_Skill()
    {
        SceneManager.LoadScene("SkillSelect");
    }
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        Application.Quit();
#endif
    }
}
