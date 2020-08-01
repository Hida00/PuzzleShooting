using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    void Start()
    {
        Invoke("BackSelect" , 8f);
    }

    void Update()
    {
        
    }
    void BackSelect()
    {
        SceneManager.LoadScene("Select");
    }
}
