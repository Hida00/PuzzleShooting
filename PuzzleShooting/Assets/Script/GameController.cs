using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    string fileName;

    void Start()
    {
        Application.targetFrameRate = 65;
    }

    void Update()
    {

    }
    public void Set()
    {
        fileName = SelectController.SelectName;
    }
}
