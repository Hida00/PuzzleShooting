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
    public int frameNum = 0;

    public bool select = false;

    void Start()
    {
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
                if(hit.gameObject.GetComponent<SkillFrame>().frameNum != frameNum && frameNum == 0)
                {
                    var obj = Instantiate(this.gameObject , hit.gameObject.transform);

                    obj.GetComponent<SkillPanels>().Parent = this.gameObject.GetComponent<SkillPanels>();
                    obj.GetComponent<RectTransform>().sizeDelta = hit.gameObject.GetComponent<RectTransform>().sizeDelta * 0.75f;
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0 , 0);
                    obj.GetComponent<SkillPanels>().frameNum = hit.gameObject.GetComponent<SkillFrame>().frameNum;

                    select = true;
                    hit.gameObject.GetComponent<SkillFrame>().SetImage(obj.GetComponent<Image>());

                    this.transform.position = StartPos;
                }
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.position = StartPos;
    }
    public void OnDrop(PointerEventData eventData)
    {
    }
}
