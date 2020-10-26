using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour
{
    public AudioSource BGM;

    public static float volume = 0.7f;

    public static string SelectName = "last";
    public static string StageImage = "image2";
    public static string MusicName = "Stage1";
    public static int[] SetSkills = { 0 , 1 , 2 };
    public static bool isHardClear = false;

    void Start()
    {
#if UNITY_EDITOR
        volume = 0;
#endif
        BGM.volume = volume;
        BGM.loop = true;
        BGM.Play();
    }

    void Update() { }

    public void Click_One()
    {
        SelectName = "Normal";
        StageImage = "image1";
        MusicName = "Stage1";
        BGM.Stop();
        SceneManager.LoadScene("PlayScene");
    }
    public void Click_Two()
    {
        SelectName = "Hard";
        StageImage = "image2";
        MusicName = "Stage2";
        BGM.Stop();
        SceneManager.LoadScene("PlayScene");
    }
    public void Click_Three()
	{
        SelectName = "last";
        StageImage = "image4";
        MusicName = "BossS";
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
    public void Title()
    {
        Quit();
    }
    void Quit()
    {
        BGM.Stop();
        SceneManager.LoadScene("Title");
    }
    public void How()
    {
        BGM.Stop();
        SceneManager.LoadScene("HowToPlay");
    }
}
