using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    public Image image;
    [NonSerialized]
    public Image img;

    Camera MainCamera;
    GameObject canvas;

    void Start()
    {
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas = GameObject.Find("Canvas");
        img = Instantiate(image , canvas.transform);
        this.transform.localScale *= 0.5f;
        img.rectTransform.sizeDelta *= 0.5f;
    }
    void Update()
    {
        img.transform.position =
            RectTransformUtility.WorldToScreenPoint(MainCamera, this.transform.position);
    }
}
