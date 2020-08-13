using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackSpeedImage : MonoBehaviour , IDragHandler , IDropHandler
{
    AttackSpeed _attackSpeed;

    Vector3 StartPos;

    public int Num;
    public int? frameNum;

    void Start()
    {
        _attackSpeed = GameObject.Find("AttackSpeed(Clone)").GetComponent<AttackSpeed>();
        if(frameNum != null) this.enabled = false;
    }

    void Update()
    {

    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
        _attackSpeed.imageNum[Num - 1] = 0;
    }
    public void OnDrop(PointerEventData eventData)
    {
        var raycastResult = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData , raycastResult);

        foreach(var hit in raycastResult)
        {
            if(hit.gameObject.CompareTag("Frame"))
            {
                _attackSpeed.imageNum[Num - 1] = Num;
                this.GetComponent<RectTransform>().anchoredPosition = hit.gameObject.GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }
}
