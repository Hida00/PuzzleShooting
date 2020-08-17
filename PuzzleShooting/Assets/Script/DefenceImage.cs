using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefenceImage : MonoBehaviour , IDragHandler
{
    public Image center;

    public Defence _defence;

    float rotation;

    public int onPeace = 0;
    public int index;
    int difIndex = -1;
    int childIndex;

    public string posNum;
    public string difNum = "-1";
    public string[] posNums;
    string childpos;

    bool isDrag;
    public bool isCenter;

    void Start()
    {
        rotation = 0f;
        if(center != null)
        {
            SetChildrenPos(center , true);
            center.transform.rotation = Quaternion.Euler(0 , 0 , rotation);
            if(isCenter)
            {
                int i = 0;
                foreach(Transform obj in center.transform)
                {
                    obj.gameObject.GetComponent<DefenceImage>().childpos = posNums[i];
                    string[] Pos = posNums[i].Split('.');
                    obj.gameObject.GetComponent<DefenceImage>().childIndex
                        = int.Parse(Pos[0]) - int.Parse(Pos[1]) * _defence.width;
                    i++;
                }
            }
        }
        else this.GetComponent<Image>().color = Color.white;
    }
    void Update()
    {
        if(center == null)
        {
            _defence.ClearPeaceNum(index);
            this.GetComponent<Image>().color = Color.white;
        }
        if(center != null)
        {
            difIndex = center.GetComponent<DefenceImage>().difIndex;
            difNum = center.GetComponent<DefenceImage>().difNum;

            if(difIndex != -1 && difNum != "-1")
            {
                if(!isCenter)
                {
                    string[] pos = SetRotationPosNum(center , childpos.Split('.')).Split('.');
                    difIndex += int.Parse(pos[0]) - int.Parse(pos[1]) * _defence.width;
                    if(!(difIndex < 0 || difIndex >= _defence.size))
                    {
                        string[] difPos = difNum.Split('.');
                        difNum = (int.Parse(pos[0]) + int.Parse(difPos[0])).ToString() + "." + (-int.Parse(pos[1]) + int.Parse(difPos[1])).ToString();
                    }
                }
                _defence.SetPeaceNum(difIndex);
                try { GameObject.Find(difNum).GetComponent<Image>().color = center.color; }
                catch { }
            }
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if(center != null)
        {
            SetChildrenPos(center , true);

            center.transform.position = data.position;

            var raycastResult = new List<RaycastResult>();
            EventSystem.current.RaycastAll(data , raycastResult);

            foreach(var hit in raycastResult)
            {
                if(hit.gameObject.CompareTag("Frame"))
                {
                    center.rectTransform.anchoredPosition = hit.gameObject.GetComponent<RectTransform>().anchoredPosition;
                    SetChildrenPos(center , false);

                    center.GetComponent<DefenceImage>().difIndex = hit.gameObject.GetComponent<DefenceImage>().index;
                    center.GetComponent<DefenceImage>().difNum = hit.gameObject.GetComponent<DefenceImage>().posNum;
                    _defence.select = center;
                    isDrag = true;
                }
            }
            if(!isDrag)
            {
                center.GetComponent<DefenceImage>().difIndex = -1;
                center.GetComponent<DefenceImage>().difNum = "-1";
            }
            isDrag = false;
        }
    }
    public void Turn()
    {
        rotation += 90f;
        if(rotation == 360) rotation = 0f;
        center.transform.rotation = Quaternion.Euler(0 , 0 , rotation);
    }
    void SetChildrenPos(Image center,bool isMove)
    {
        float prov = Screen.height / 450f;
        int i = 0;
        foreach(Transform obj in center.transform)
        {
            string[] abs = center.GetComponent<DefenceImage>().posNums[i].Split('.');
            if(isMove)
            {
                obj.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(25f * float.Parse(abs[0]) * prov , 25f * float.Parse(abs[1]) * prov);
            }
            else
            {
                obj.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(30f * float.Parse(abs[0]) * prov , 30f * float.Parse(abs[1]) * prov);
            }
            i++;
        }
    }
    void SetChildrenNum(Image center,int hitIndex,int index)
    {

    }
    string SetRotationPosNum(Image center , string[] pos)
    {
        float rotation = center.rectTransform.rotation.eulerAngles.z;
        if(rotation == 90f)
        {
            return Swap(int.Parse(pos[0]) , int.Parse(pos[1]) , 1);
        }
        else if(rotation == 180f)
        {
            return Swap(int.Parse(pos[0]) , int.Parse(pos[1]) , 2);
        }
        else if(rotation == 270f)
        {
            return Swap(int.Parse(pos[0]) , int.Parse(pos[1]) , 3);
        }
        return pos[0] + "." + pos[1];
    }
    string Swap(int x , int y , int count)
    {
        for(int i = 0; i < count; i++)
        {
            int buff = x;
            x = -y;
            y = buff;
        }
        string buf = (x).ToString() + "." + y.ToString();
        return buf;
    }
}
