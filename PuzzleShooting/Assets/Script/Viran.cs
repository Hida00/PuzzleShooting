using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Viran : MonoBehaviour
{
    public GameObject bullet;
    GameController _gameController;

    public float ViranHealth = 100f;
    public float maxHealth;
    public float defensePoint = 2f;
    public float displaceTime;
    public float changeTime;
    float startTime;
    public float speed = 4f;
    public float BulletAngle;
    public float MoveAngleZ1;
    public float MoveAngleZ2;

    int frameCount = 0;
    public int interval = 15;
    public int Type;
    public int score;


    void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();

        maxHealth = ViranHealth;
        defensePoint = 2f;
        startTime = Time.time;
    }

    void Update()
    {
        if(ViranHealth <= 0f)
        {
            _gameController._score += score;

            Destroy(this.gameObject);
        }
        if(Time.time - startTime >= displaceTime)
        {
            Destroy(this.gameObject);
        }
        if(Time.time - startTime >= changeTime)
        {
            MoveAngleZ1 = MoveAngleZ2;
        }
        frameCount++;
        if(frameCount == interval && Type == 2 && speed != 0)
        {
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0,0,BulletAngle));
            frameCount = 0;
        }
        if(frameCount == interval && speed == 0)
        {
            frameCount = 0;
            Vector3 pos = GameObject.Find("Player").transform.position;

            float angle = (float)(Math.Atan2(this.transform.position.y - pos.y , this.transform.position.x - pos.x) * 180f / Math.PI);

            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 90f + angle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 105f + angle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 75f + angle));
        }
        if(frameCount == interval && Type == 1 && speed != 0)
        {
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 15f));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle - 15f));
            frameCount = 0;
        }
        if(this.transform.position.x >= 9.2f || this.transform.position.x <= -9.2f || this.transform.position.y >= 11f || this.transform.position.y <= -11f)
        {
            Destroy(this.gameObject);
        }
        this.transform.position += new Vector3((float)Math.Sin(MoveAngleZ1 * Math.PI / 180) * speed * Time.deltaTime , (float)Math.Cos(MoveAngleZ1 * Math.PI / 180) * speed * Time.deltaTime , 0);
    }
}
