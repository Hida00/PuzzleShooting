using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Generator : MonoBehaviour
{
    public GameObject viran1;
    public GameObject viran2;
    public GameObject boss;
    public GameObject midBoss;

    GameController _gameController;
    public TextMeshProUGUI BossAlert;

    public Slider bossHP;

    string fileName = "Hard11";

    public int Ecount = 0;
    int viranCount = 0;
    int count = 0;

    public float startTime;
    string[] Data;

    public bool wait = true;
    bool isReader = true;

    StringReader data;

    void Start()
    {
        startTime = Time.time;
        fileName = SelectController.SelectName;
        wait = true;

        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        TextAsset csv = Resources.Load(@"CSV/StageData/" + fileName) as TextAsset;
        data = new StringReader(csv.text);
        Data = new string[20];
        data.ReadLine();
        data.ReadLine();
    }

    void Update()
    {
        if(isReader)
        {
            Data = data.ReadLine().Split(',');
            isReader = false;
        }

        bool isSkill = GameObject.Find("PanelController").GetComponent<PanelController>().isSkill;
        float dif = Time.time - startTime;

        if(float.Parse(Data[0]) == 1f && (float.Parse(Data[6]) - dif) <= 0.1f && wait && !isSkill)
        {
            var obj = Instantiate(viran1);
            

            obj.transform.position = new Vector3(float.Parse(Data[1]) , float.Parse(Data[2]) , float.Parse(Data[3]));
            obj.GetComponent<Viran>().MoveAngle = float.Parse(Data[4]);
            obj.GetComponent<Viran>().BulletAngle = float.Parse(Data[5]);
            obj.GetComponent<Viran>().changeTime = float.Parse(Data[7]);
            obj.GetComponent<Viran>().displaceTime = float.Parse(Data[8]);
            obj.GetComponent<Viran>().speed = float.Parse(Data[9]);
            obj.GetComponent<Viran>().Type = int.Parse(Data[0]);
            obj.GetComponent<Viran>().ViranHealth = float.Parse(Data[10]);
            obj.GetComponent<Viran>().interval = int.Parse(Data[11]);
            obj.GetComponent<Viran>().score = int.Parse(Data[12]);
            obj.GetComponent<Viran>().AngleAbs = float.Parse(Data[13]);
            obj.GetComponent<Viran>().isFinal = int.Parse(Data[14]);
            obj.GetComponent<Viran>().damage = float.Parse(Data[15]);
            obj.GetComponent<Viran>().MoveAngles = new float[int.Parse(Data[16])];
            int add = int.Parse(Data[16]);
            for(int i = 0; i < add; i++)
            {
                obj.GetComponent<Viran>().MoveAngles[i] = float.Parse(Data[17 + i]);
            }
            obj.GetComponent<Viran>().imageName = Data[17 + add];

            if((int)float.Parse(Data[14]) == 1)
            {
                wait = false;
            }

            isReader = true;
            viranCount++;
            count++;
        }
        if(float.Parse(Data[0]) == 2f && (float.Parse(Data[6]) - dif) <= 0.1f && wait && !isSkill)
        {
            var obj = Instantiate(viran2);

            obj.transform.position = new Vector3(float.Parse(Data[1]) , float.Parse(Data[2]) , float.Parse(Data[3]));
            obj.GetComponent<Viran>().MoveAngle = float.Parse(Data[4]);
            obj.GetComponent<Viran>().BulletAngle = float.Parse(Data[5]);
            obj.GetComponent<Viran>().changeTime = float.Parse(Data[7]);
            obj.GetComponent<Viran>().displaceTime = float.Parse(Data[8]);
            obj.GetComponent<Viran>().speed = float.Parse(Data[9]);
            obj.GetComponent<Viran>().Type = int.Parse(Data[0]);
            obj.GetComponent<Viran>().ViranHealth = float.Parse(Data[10]);
            obj.GetComponent<Viran>().interval = int.Parse(Data[11]);
            obj.GetComponent<Viran>().score = int.Parse(Data[12]);
            obj.GetComponent<Viran>().AngleAbs = float.Parse(Data[13]);
            obj.GetComponent<Viran>().isFinal = int.Parse(Data[14]);
            obj.GetComponent<Viran>().damage = float.Parse(Data[15]);
            obj.GetComponent<Viran>().MoveAngles = new float[int.Parse(Data[16])];
            int add = int.Parse(Data[16]);
            for(int i = 0;i < int.Parse(Data[16]);i++)
            {
                try { obj.GetComponent<Viran>().MoveAngles[i] = float.Parse(Data[17 + i]); }
                catch { Debug.Log(viranCount); }
            }
            obj.GetComponent<Viran>().imageName = Data[17 + add];
            if(int.Parse(Data[14]) == 1)
            {
                wait = false;
            }

            isReader = true;
            viranCount++;
            count++;
        }
        if(float.Parse(Data[0]) == 3f && (float.Parse(Data[6]) - dif) <= 0.1f && wait && !isSkill)//Boss
        {
            bossHP.gameObject.SetActive(true);
            Destroy(GameObject.Find("BossAlert"));

            var obj = Instantiate(boss);
            obj.transform.position = new Vector3(float.Parse(Data[1]) , float.Parse(Data[2]) , float.Parse(Data[3]));
            obj.GetComponent<Boss>().MoveAngle = float.Parse(Data[4]);
            obj.GetComponent<Boss>().rotationCount = float.Parse(Data[5]);
            obj.GetComponent<Boss>().timeSpan = float.Parse(Data[7]);
            obj.GetComponent<Boss>().speed = float.Parse(Data[8]);
            obj.GetComponent<Boss>().bossHealth = float.Parse(Data[9]);
            obj.GetComponent<Boss>().interval = int.Parse(Data[10]);
            obj.GetComponent<Boss>().skillCount = int.Parse(Data[11]);
            obj.GetComponent<Boss>().score = int.Parse(Data[12]);
            obj.GetComponent<Boss>().damage = float.Parse(Data[14]);
            obj.GetComponent<Boss>().imageName = Data[15];
            obj.GetComponent<Boss>().scale = float.Parse(Data[16]);

            for(int i = 0; i < int.Parse(Data[11]); i++)
            {
                float[] array = data.ReadLine().Split(',').Select(float.Parse).ToArray();
                obj.GetComponent<Boss>().skillData.Add(array);
            }
            _gameController.BGM.Stop();

            wait = false;
            isReader = true;
            viranCount++;
            count++;
        }
        if(float.Parse(Data[0]) == 4f && (float.Parse(Data[3]) - dif) <= 0.1f && wait && !isSkill)//MidBoss
        {
            var obj = Instantiate(midBoss);

            obj.transform.position = new Vector3(float.Parse(Data[1]) , float.Parse(Data[2]) , float.Parse(Data[3]));
            obj.GetComponent<MidBoss>().HealthPoint = float.Parse(Data[5]);
            obj.GetComponent<MidBoss>().interval = int.Parse(Data[6]);
            obj.GetComponent<MidBoss>().score = int.Parse(Data[7]);
            obj.GetComponent<MidBoss>().isFinal = int.Parse(Data[8]);
            obj.GetComponent<MidBoss>().TimeSpan = float.Parse(Data[9]);
            obj.GetComponent<MidBoss>().scale = new Vector3(float.Parse(Data[10]) , float.Parse(Data[10]) , float.Parse(Data[10]));
            obj.GetComponent<MidBoss>().damage = float.Parse(Data[11]);
            obj.GetComponent<MidBoss>().imageName = Data[11];

            isReader = true;
            viranCount++;
            count++;
            if(int.Parse(Data[8]) == 1)
            {
                wait = false;
            }
        }
        if(float.Parse(Data[0]) == 5f && (dif - float.Parse(Data[4])) <= 0.1f && wait && !isSkill)
        {
            float prov = (float)Screen.height / 450;

            var obj = Instantiate(BossAlert , GameObject.Find("Canvas").transform);

            obj.rectTransform.sizeDelta *= prov;
            obj.rectTransform.anchoredPosition
                = new Vector3(float.Parse(Data[1]) , float.Parse(Data[2]) , float.Parse(Data[3]));
            obj.rectTransform.anchoredPosition *= prov;
            obj.fontSize *= prov;
            obj.name = "BossAlert";

            isReader = true;
            viranCount++;
            count++;
        }

        if(count == Ecount && !wait)
        {
            startTime = Time.time;
            wait = true;
        }
    }
}
