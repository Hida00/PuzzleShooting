using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Viran : MonoBehaviour
{
    public GameObject bullet;
    public ParticleSystem particle;
    GameController _gameController;
    PanelController _panelController;

    public float[] MoveAngles;
    public float ViranHealth = 100f;
    public float maxHealth;
    public float defensePoint = 2f;
    public float displaceTime;
    public float changeTime;
    public float startTime;
    public float speed = 4f;
    public float BulletAngle;
    public float AngleAbs;
    public float difTime;
    public float MoveAngle;

    int frameCount = 0;
    public int interval = 15;
    public int Type;
    public int score;
    public int isFinal;
    int count = 0;
    int maxCount;

    void Start()
    {
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();

        maxHealth = ViranHealth;
        defensePoint = 2f;
        startTime = Time.time;
        maxCount = MoveAngles.Length;
    }
    
    void Update()
    {
        if(!_panelController.isSkill) frameCount++;
        if(ViranHealth <= 0f)
        {
            GameObject.Find("Generator").GetComponent<Generator>().Ecount++;
            _gameController._score += score;
            Instantiate(particle , this.transform.position , Quaternion.Euler(90 , 0 , 0));
            Destroy(this.gameObject);
        }
        if(Time.time - startTime >= changeTime && !_panelController.isSkill)
        {
            startTime = Time.time;
            MoveAngle = MoveAngles[count];
            count++;
        }
        if((Time.time - startTime) >= displaceTime && !_panelController.isSkill)
        {
            GameObject.Find("Generator").GetComponent<Generator>().Ecount++;
            Destroy(this.gameObject);
        }
        if(frameCount == interval && Type == 2 && speed != 0
            && !GameObject.Find("PanelController").GetComponent<PanelController>().isSkill)
        {
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0,0,BulletAngle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0,0,BulletAngle + 25));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0,0,BulletAngle - 25f));
            frameCount = 0;
        }
        if(frameCount == interval && speed == 0 && !_panelController.isSkill)
        {
            frameCount = 0;
            Vector3 pos = GameObject.Find("Player").transform.position;
            float angle = (float)(Math.Atan2(this.transform.position.y - pos.y , this.transform.position.x - pos.x) * 180f / Math.PI);

            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 ,  90f + angle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 115f + angle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 140f + angle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 ,  65f + angle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 ,  40f + angle));
        }
        if(frameCount == interval && Type == 1 && speed != 0 && !_panelController.isSkill)
        {
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 20f));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle - 20f));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 40f));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle - 40f));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 60f));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle - 60f));
            frameCount = 0;
        }
        if(this.transform.position.x >= 13f || this.transform.position.x <= -13f || this.transform.position.y >= 15f || this.transform.position.y <= -15f || count >= maxCount)
        {
            GameObject.Find("Generator").GetComponent<Generator>().Ecount++;
            Destroy(this.gameObject);
        }
        float skill = _panelController.skillSpeed;
        this.transform.position += new Vector3((float)Math.Sin(MoveAngle * Math.PI / 180) * speed * Time.deltaTime , (float)Math.Cos(MoveAngle * Math.PI / 180) * speed * Time.deltaTime , 0) * skill;
    }
}
