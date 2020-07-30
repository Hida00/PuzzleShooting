using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class Strength : MonoBehaviour
{
    public Image[] panels;

    GameObject panel;

    PanelController _panelController;

    public PlayerController _playerController;

    [NonSerialized]
    public Vector3 start;

    [NonSerialized]
    public int[] isConnect;

    int[] target;
    int lineCount = 1;
    int size;

    [NonSerialized]
    public int buf;

    public bool isClick = false;
    public bool isSuccess = false;

    public TextMeshProUGUI ClearText;

    void Start()
    {
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
        panel = GameObject.Find("Panel");
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Create_Image();

        Invoke("Finish" , 15f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Finish();
        if (isSuccess)
        {
            var obj = Instantiate(ClearText , new Vector3(0f , 0f , 0f) , Quaternion.identity);
            obj.transform.SetParent(panel.transform , false);
            obj.rectTransform.anchoredPosition = new Vector2(0f , 0f);

            Invoke("strength" , 0.8f);
        }
        Check_Connect();
    }
    void Create_Image()
    {
        int i = 0;
        TextAsset csv = Resources.Load(@"CSV/Strength/Strength") as TextAsset;
        StringReader st = new StringReader(csv.text);
        string[] info = st.ReadLine().Split(',');
        size = int.Parse(info[0]);
        isConnect = new int[size];
        float prov = (float)Screen.height / 450;

        while (st.Peek() > -1 || i < size)
        {
            string[] values = st.ReadLine().Split(',');
            var obj = Instantiate(panels[0] , new Vector3(0f , 0f , 0f) , Quaternion.identity);
            obj.transform.SetParent(panel.transform , false);
            obj.rectTransform.anchoredPosition = new Vector2(float.Parse(values[1]) * prov , float.Parse(values[2]) * prov);

            obj.rectTransform.sizeDelta *= new Vector2(prov , prov);

            obj.GetComponent<StrengthImage>().Num = i;
            i++;
        }

        var sample = Instantiate(panels[3] , new Vector3(0 , 0 , 0) , Quaternion.identity);
        sample.transform.SetParent(panel.transform , false);
        sample.rectTransform.anchoredPosition = new Vector2(-150f , 0f);
        sample.rectTransform.sizeDelta *= new Vector2(prov * 0.8f, prov * 0.8f);

        var sampleCSV = Resources.Load(@"CSV/Strength/sample") as TextAsset;
        StringReader st2 = new StringReader(sampleCSV.text);
        System.Random r = new System.Random();
        List<string[]> list = new List<string[]>();
        info = st2.ReadLine().Split(',');
        int num = r.Next(0 , int.Parse(info[2]));

        target = new int[int.Parse(info[1])];
        for (i = 0; i < int.Parse(info[2]); i++)
        {
            list.Add(st2.ReadLine().Split(','));
        }
        for(i = 0;i < int.Parse(info[1]);i++)
        {
            target[i] = int.Parse(list[num][i + 2]);
        }
        sample.GetComponent<Image>().sprite = Resources.Load<Sprite>(@"Image/Strength/" + list[num][1]);

    }
    void Check_Connect()
    {
        int count = 0;
        for(int i = 0;i < target.Length;i++)
        {
            if (target[i] == isConnect[i])
            {
                count++;
            }
        }
        if(count == size)
        {
            isSuccess = true;
        }
    }
    public void DrawLine(Vector3 end , int num)
    {
        Vector3 diff = end - start;
        float length = (float)Math.Sqrt(diff.x * diff.x + diff.y * diff.y);

        var obj = Instantiate(panels[1] , new Vector3(0f , 0f , 0f) , Quaternion.identity);

        obj.transform.SetParent(panel.transform , false);
        obj.rectTransform.localScale = new Vector3(length / 50f , 1f , 1f);
        obj.rectTransform.anchoredPosition = (start + end) / 2;
        obj.rectTransform.rotation = Quaternion.Euler(0 , 0 , (float)(Math.Atan2(diff.y , diff.x) * (180 / Math.PI)));

        var tri = Instantiate(panels[2] , new Vector3(0 , 0 , 0) , Quaternion.identity);
        tri.transform.SetParent(panel.transform , false);
        tri.rectTransform.anchoredPosition = (start + end) / 2;
        tri.rectTransform.rotation = Quaternion.Euler(0 , 0 , (float)(Math.Atan2(diff.y , diff.x) * (180 / Math.PI)));

        start = end;
        lineCount++;
        isConnect[num] = lineCount;
    }
    void strength()
    {
        StartCoroutine("Player_Strength_Up");
        Finish();
    }
    void Finish()
    {
        _panelController.isSkill = false;
        //スキル使用時に遅くなった時間を戻す
        Time.timeScale = 1.0f;

        foreach (Transform n in panel.transform)
        {
         
            Destroy(n.gameObject);
        }
        target = new int[0];
        isSuccess = false;
    }
    private IEnumerator Player_Strength_Up()
    {
        _playerController.strength = 2.0f;
        yield return new WaitForSeconds(20f);
        _playerController.strength = 1.0f;
        Destroy(this.gameObject);
    }
}
