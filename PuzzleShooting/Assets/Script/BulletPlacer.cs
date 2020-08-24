using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BulletPlacer : MonoBehaviour
{
    public GameObject Bullet;
    public Image Blue;
    GameObject _player;
    PanelController _panelController;

    public GameObject _boss;

    public float[] skillData;
    public float dif;
    public float speed;
    public float damage;
    float x, y;
    float[] data;

    public int interval = 10;
    int frameCount = 0;
    int Count = 0;

    bool move = true;
    bool[] boolen;
    public bool isLockOn = false;

    void Start()
    {
        _player = GameObject.Find("Player");
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();

        data = new float[10];
        boolen = new bool[10];
        Count = 0;
        if(!isLockOn)
        {
            skillData[1] += dif;
            move = true;
        }
        else
        {
            data[1] = this.transform.position.x / Mathf.Abs(this.transform.position.x);
        }
        boolen = boolen.Select(x => true).ToArray();
    }

    void Update()
    {
        frameCount++;
        if(isLockOn && move && !_panelController.isSkill) LockOn();
        else if(!_panelController.isSkill)
        {
            if(skillData[0] == 1 && move) Circle(skillData[1] + dif);
            if(skillData[0] == 2 && move) Star(skillData[1] + dif);
            if(skillData[0] == 3 && move) BigCircle(skillData[1] + dif);
        }

        if(frameCount >= interval && !isLockOn && !_panelController.isSkill)
        {
            float angle = (float)Math.Atan2(this.transform.position.y - _player.transform.position.y , this.transform.position.x - _player.transform.position.x) * 180f / (float)Math.PI;
            var obj = Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , 90f + angle));
            obj.GetComponent<BulletController>().isBoss = true;
            obj.GetComponent<BulletController>().speed = skillData[4];
            obj.GetComponent<BulletController>().bulletImage = Blue;
            frameCount = 0;
        }
    }
    void LockOn()
    {
        if(frameCount >= interval)
        {
            var obj = Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , 0f + Count));
            obj.GetComponent<BulletController>().speed = speed;
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , 25f + Count));
            obj.GetComponent<BulletController>().speed = speed;
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , -25f + Count));
            obj.GetComponent<BulletController>().speed = speed;
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , 50f + Count));
            obj.GetComponent<BulletController>().speed = speed;
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , -50f + Count));
            obj.GetComponent<BulletController>().speed = speed;
            obj.GetComponent<BulletController>().damagePoint = damage;
            frameCount = 0;
            Count += 20 * (int)data[1];
            if(Mathf.Abs(Count) > 180) Count = 0;
        }
    }
    void Circle(float radius)
    {
        if(Count == skillData[3]) Destroy(this.gameObject);
        if(!boolen[0] && data[0] + (2 * Mathf.PI) <= Time.time * speed)
        {
            move = false;
            this.transform.position = new Vector3(skillData[1] , _boss.transform.position.y);
            skillData[1] += skillData[2];
            MoveStart();
            Count++;
            interval -= 2;
            boolen[0] = true;
        }
        x = radius * Mathf.Sin(Time.time * speed) + _boss.transform.position.x;
        y = radius * Mathf.Cos(Time.time * speed) + _boss.transform.position.y;
        this.transform.position = new Vector3(x , y);
        if(boolen[0])
        {
            data[0] = Time.time * speed;
            boolen[0] = false;
        }
    }
    void Star(float radius)
    {
        if(data[1] == 3) Destroy(this.gameObject);
        float X = this.transform.position.x;
        float Y = this.transform.position.y;
        if((radius) * (radius) <= X * X + Y * Y)
        {
            boolen[0] = true;
        }
        if(boolen[0])
        {
            Count++;
            if(Count == 1)
            {
                this.transform.position = new Vector3(0 , radius - 1);
                data[0] = -162f;
            }
            else if(Count == 2) data[0] = 54f;
            else if(Count == 3) data[0] = -90f;
            else if(Count == 4) data[0] = 126f;
            else if(Count == 5) data[0] = -18f;
            else if(Count == 6)
            {
                this.transform.position = new Vector3(skillData[1] , _boss.transform.position.y);
                skillData[1] += skillData[2];
                MoveStart();
                Count = 0;
                data[1]++;
            }
            boolen[0] = false;
        }
        this.transform.position += new Vector3(Mathf.Sin(data[0] * Mathf.PI / 180) * speed * Time.deltaTime , Mathf.Cos(data[0] * Mathf.PI / 180) * speed * Time.deltaTime , 0);
    }
    void BigCircle(float radius)
    {
        if(Count == skillData[3]) Destroy(this.gameObject);
        if(!boolen[0] && data[0] + (2 * Mathf.PI) <= Time.time * speed)
        {
            move = false;
            skillData[1] += skillData[2];
            MoveStart();
            Count++;
            interval -= 2;
            boolen[0] = true;
        }
        x = radius * Mathf.Sin(Time.time * speed) + _boss.transform.position.x;
        y = radius * Mathf.Cos(Time.time * speed) + _boss.transform.position.y;
        this.transform.position = new Vector3(x , y);
        if(boolen[0])
        {
            data[0] = Time.time * speed;
            boolen[0] = false;
        }
    }
    void MoveStart()
    {
        GameObject.Find("GameController").GetComponent<GameController>().BossBulletMoveStart();
        move = true;
    }
}
