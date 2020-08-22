using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour
{
    public AudioSource BGM;

    public static float volume = 0;//.7f;

    public static string SelectName = "Hard1";
    public static string StageImage = "image2";
    public static string MusicName = "Stage1";
    public static int[] SetSkills = { 0 , 1 , 2 };

    void Start()
    {
        BGM.volume = volume;
        BGM.loop = true;
        BGM.Play();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) Quit();
    }
    public void Click_One()
    {
        SelectName = "Normal1";
        StageImage = "image1";
        MusicName = "Stage1";
        BGM.Stop();
        SceneManager.LoadScene("PlayScene");
    }
    public void Click_Two()
    {
        SelectName = "Hard1";
        BGM.Stop();
        SceneManager.LoadScene("PlayScene");
    }
    public void Click_Setting()
    {
        BGM.Stop();
        SceneManager.LoadScene("Setting");
    }
    public void Click_Skill()
    {
        BGM.Stop();
        SceneManager.LoadScene("SkillSelect");
    }
    void Quit()
    {
        BGM.Stop();
        SceneManager.LoadScene("Title");
    }
}
