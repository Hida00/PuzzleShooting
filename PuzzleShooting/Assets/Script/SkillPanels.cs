using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillPanels : MonoBehaviour , IDropHandler , IDragHandler , IBeginDragHandler , IEndDragHandler
{
    SkillSelect _skillSelect;

    public SkillPanels Parent;

    public Vector3 StartPos;

    public int Num = 1;

    public bool select = false;

    void Start()
    {
        float prov = (float)Screen.height / 450;
        this.GetComponent<RectTransform>().sizeDelta *= new Vector2(prov , prov);

        _skillSelect = GameObject.Find("SkillSelect").GetComponent<SkillSelect>();
    }
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StartPos = this.transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;

        var raycastResult = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData , raycastResult);

        foreach(var hit in raycastResult)
        {
            if(hit.gameObject.CompareTag("Frame") && !select)
            {
                var obj = Instantiate(this.gameObject);

                obj.GetComponent<SkillPanels>().Parent = this.gameObject.GetComponent<SkillPanels>();
                obj.GetComponent<RectTransform>().sizeDelta = hit.gameObject.GetComponent<RectTransform>().sizeDelta * 0.75f;
                obj.transform.SetParent(hit.gameObject.transform , false);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0 , 0);

                select = true;
                hit.gameObject.GetComponent<SkillFrame>().SetImage(obj.GetComponent<Image>());

                this.transform.position = StartPos;
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.position = StartPos;
    }
    public void OnDrop(PointerEventData eventData)
    {
        var raycastResult = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData , raycastResult);

        foreach(var hit in raycastResult)
        {
            if(hit.gameObject.CompareTag("Frame"))
            {
                this.transform.position = hit.gameObject.transform.position;
                this.enabled = false;
            }
        }
    }
}
