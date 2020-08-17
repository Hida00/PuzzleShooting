using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackSpeed : MonoBehaviour
{
    GameObject panel;

    PanelController _panelController;
    PlayerController _playerController;

    public Image peace;
    public Image frame;

    public TextMeshProUGUI ClearText;

    int size;
    int[] target;
    public int[] imageNum;

    bool isSuccess = false;

    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        panel = GameObject.Find("Panel");
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();

        Create_Image();

        Invoke("Finish" , 15f);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) Finish();
        if(isSuccess)
        {
            var obj = Instantiate(ClearText);
            obj.transform.SetParent(panel.transform , false);
            obj.rectTransform.anchoredPosition = new Vector2(0 , -70f);

            Invoke("Succese" , 0.8f);
        }
        Check_Array();
    }
    void Create_Image()
    {
        int i = 0;
        TextAsset csv = Resources.Load(@"CSV/AttackSpeed/AttackSpeed") as TextAsset;
        StringReader st = new StringReader(csv.text);
        string[] info = st.ReadLine().Split(',');

        float prov = (float)Screen.height / 450f;
        System.Random r = new System.Random();
        int minX = int.Parse(info[2]);
        int maxX = int.Parse(info[3]) + 1;
        int minY = int.Parse(info[4]);
        int maxY = int.Parse(info[5]) + 1;

        size = int.Parse(info[1]);
        target = new int[size];
        imageNum = new int[size];
        List<Image> frames = new List<Image>();
        List<Image> peaces = new List<Image>();

        while(st.Peek() > -1)
        {
            string[] values =  st.ReadLine().Split(',');

            var obj = Instantiate(peace);
            peaces.Add(obj);
            obj.rectTransform.sizeDelta *= new Vector2(prov , prov);
            obj.rectTransform.anchoredPosition = new Vector2(r.Next(minX , maxX) , r.Next(minY , maxY));
            obj.GetComponent<AttackSpeedImage>().Num = i + 1;
            Sprite sprite = Resources.Load<Sprite>(@"Image/AttackSpeed/image" + r.Next(1,1).ToString() + "[" + (i + 1).ToString() + "]");
            obj.sprite = sprite;

            var obj2 = Instantiate(frame);
            frames.Add(obj2);
            obj2.rectTransform.anchoredPosition = new Vector2(float.Parse(values[2]) , float.Parse(values[3])) * prov;
            obj2.rectTransform.sizeDelta *= new Vector2(prov , prov);
            obj2.GetComponent<AttackSpeedImage>().frameNum = i;
            i++;
        }
        foreach(var obj in frames)
        {
            obj.transform.SetParent(panel.transform , false);
        }
        foreach(var obj in peaces)
        {
            obj.transform.SetParent(panel.transform , false);
        }
        for(i = 0;i < size;i++)
        {
            target[i] = i + 1;
        }
    }
    void Finish()
    {
        panel.SetActive(false);
        _panelController.skillSpeed = 1;
        //スキル使用時に遅くなった時間を戻す
        Time.timeScale = 1.0f;

        foreach(Transform n in panel.transform)
        {
            Destroy(n.gameObject);
        }
        _panelController.FinishTimeSet();
        Destroy(this.gameObject);
    }
    void Check_Array()
    {
        int count = 0;
        for(int i = 0;i < size;i++)
        {
            if(target[i] == imageNum[i]) count++;
        }
        if(count == size) isSuccess = true;
    }
    void Succese()
    {
        _playerController.AttackSpeed();
        Finish();
    }
}
