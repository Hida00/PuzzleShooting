using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour
{
    public static string SelectName;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Click_One()
    {
        SelectName = "Normal";
        SceneManager.LoadScene("PlayScene");
    }
    public void Click_Setting()
    {
        SceneManager.LoadScene("Setting");
    }
}
