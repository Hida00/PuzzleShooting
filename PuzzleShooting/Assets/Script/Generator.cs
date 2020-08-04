using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public GameObject viran1;
    public GameObject viran2;
    public GameObject boss;

    public Slider bossHP;

    List<float[]> viranData = new List<float[]>();
    List<float[]> BossSkill = new List<float[]>();

    int[] viranType;
    int viranCount = 0;

    float startTime;

    void Start()
    {
        startTime = Time.time;
        Create_Viran();
    }

    void Update()
    {
        float dif = Time.time - startTime;
        if(viranType[viranCount] == 1 && Math.Abs(dif - viranData[viranCount][6]) <= 0.1f)
        {
            var obj = Instantiate(viran2);
            obj.transform.position = new Vector3(viranData[viranCount][0] , viranData[viranCount][1] , viranData[viranCount][2]);
            obj.GetComponent<Viran>().MoveAngleZ1 = viranData[viranCount][3];
            obj.GetComponent<Viran>().MoveAngleZ2 = viranData[viranCount][4];
            obj.GetComponent<Viran>().BulletAngle = viranData[viranCount][5];
            obj.GetComponent<Viran>().changeTime = viranData[viranCount][7];
            obj.GetComponent<Viran>().displaceTime = viranData[viranCount][8];
            obj.GetComponent<Viran>().speed = viranData[viranCount][9];
            obj.GetComponent<Viran>().Type = viranType[viranCount];
            obj.GetComponent<Viran>().ViranHealth = viranData[viranCount][10];
            obj.GetComponent<Viran>().interval = (int)viranData[viranCount][11];
            obj.GetComponent<Viran>().score = (int)viranData[viranCount][12];
            viranCount++;
        }
        if(viranType[viranCount] == 2 && Math.Abs(dif - viranData[viranCount][6]) <= 0.1f)
        {
            var obj = Instantiate(viran2);
            obj.transform.position = new Vector3(viranData[viranCount][0] , viranData[viranCount][1] , viranData[viranCount][2]);
            obj.GetComponent<Viran>().MoveAngleZ1 = viranData[viranCount][3];
            obj.GetComponent<Viran>().MoveAngleZ2 = viranData[viranCount][4];
            obj.GetComponent<Viran>().BulletAngle = viranData[viranCount][5];
            obj.GetComponent<Viran>().changeTime = viranData[viranCount][7];
            obj.GetComponent<Viran>().displaceTime = viranData[viranCount][8];
            obj.GetComponent<Viran>().speed = viranData[viranCount][9];
            obj.GetComponent<Viran>().Type = viranType[viranCount];
            obj.GetComponent<Viran>().ViranHealth = viranData[viranCount][10];
            obj.GetComponent<Viran>().interval = (int)viranData[viranCount][11];
            obj.GetComponent<Viran>().score = (int)viranData[viranCount][12];
            viranCount++;
        }
        if(viranType[viranCount] == 3 && Math.Abs(dif - viranData[viranCount][5]) <= 0.1f)//Boss
        {
            bossHP.gameObject.SetActive(true);

            var obj = Instantiate(boss);
            obj.transform.position = new Vector3(viranData[viranCount][0] , viranData[viranCount][1] , viranData[viranCount][2]);
            obj.GetComponent<Boss>().MoveAngle = viranData[viranCount][3];
            obj.GetComponent<Boss>().rotationCount = viranData[viranCount][4];
            obj.GetComponent<Boss>().timeSpan = viranData[viranCount][6];
            obj.GetComponent<Boss>().speed = viranData[viranCount][7];
            obj.GetComponent<Boss>().bossHealth = viranData[viranCount][8];
            obj.GetComponent<Boss>().interval = (int)viranData[viranCount][9];
            obj.GetComponent<Boss>().skillCount = (int)viranData[viranCount][10];
            obj.GetComponent<Boss>().score = (int)viranData[viranCount][11];

            obj.GetComponent<Boss>().skillData = BossSkill;
            viranCount++;
        }
    }
    void Create_Viran()
    {
        int i = 0;

        var csvFile = Resources.Load(@"CSV/StageData/Normal1") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        string[] info = reader.ReadLine().Split(',');
        viranType = new int[int.Parse(info[2])];

        while(reader.Peek() != -1)
        {
            string[] values = reader.ReadLine().Split(',');
            viranType[i] = int.Parse(values[0]);
            if(viranType[i] != 3)
            {
                float[] array =
                {
                    float.Parse(values[2]),float.Parse(values[3]),float.Parse(values[4]),//position(x,y,z),(0,1,2)
                    float.Parse(values[6]),     //MoveAngle1     ,3
                    float.Parse(values[7]),     //MoveAngle2     ,4
                    float.Parse(values[9]),     //BulletRotation ,5
                    float.Parse(values[11]),    //spawnTime     ,6
                    float.Parse(values[12]),    //changeAngle   ,7
                    float.Parse(values[14]),    //displaceTime  ,8
                    float.Parse(values[16]),    //speed         ,9
                    float.Parse(values[18]),    //HealthPoint   ,10
                    float.Parse(values[20]),    //interval      ,11
                    float.Parse(values[22]),    //score         ,12
                };
                viranData.Add(array);
            }
            else
            {
                float[] array =
                {
                    float.Parse(values[2]),float.Parse(values[3]),float.Parse(values[4]),
                                                //position              ,(0,1,2)
                    float.Parse(values[6]),     //moveAngle             ,3
                    float.Parse(values[7]),     //angle per rotation    ,4
                    float.Parse(values[9]),     //spawnTime             ,5
                    float.Parse(values[10]),    //changeAngleTimespan   ,6
                    float.Parse(values[12]),    //speed                 ,7
                    float.Parse(values[14]),    //HealthPoint           ,8
                    float.Parse(values[16]),    //interval              ,9
                    float.Parse(values[18]),    //SkillCount            ,10
                    float.Parse(values[20]),    //score                 ,11 
                };
                int Count = int.Parse(values[18]);

                for(int j = 0;j < Count;j++)
                {
                    string[] values2 = reader.ReadLine().Split(',');

                    float[] ray =
                    {
                        float.Parse(values2[2]),    //ボススキルナンバー
                        float.Parse(values2[3]),    //各スキルの情報
                        float.Parse(values2[4]),    
                        float.Parse(values2[5]),
                        float.Parse(values2[6]),
                        float.Parse(values2[7]),
                        float.Parse(values2[8]),
                        float.Parse(values2[9]),
                        float.Parse(values2[10]),
                    };
                    BossSkill.Add(ray);
                }
                viranData.Add(array);
            }
            i++;
        }
    }
}
