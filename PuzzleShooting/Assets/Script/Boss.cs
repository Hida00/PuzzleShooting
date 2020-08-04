using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject Bullet;
    Slider bossHP;
    GameObject _player;

    public float MoveAngle;
    public float BulletAngle;
    public float rotationCount;
    public float bossHealth = 500f;
    public float timeSpan;
    public float speed;
    public float skillInterval;
    public float defensePoint = 10f;
    float StartTime;
    float maxHealth;

    public List<float[]> skillData = new List<float[]>();

    public int interval;
    public int skillCount;
    public int score;
    int frameCount = 0;

    void Start()
    {
        _player = GameObject.Find("Player");
        bossHP = GameObject.Find("bossHP").GetComponent<Slider>();
        StartTime = Time.time;
        maxHealth = bossHealth;
        HPcolorChange(skillCount);
    }

    void Update()
    {
        frameCount++;
        if(bossHealth <= 0f)
        {
            HPcolorChange(skillCount);
            if(skillCount == 0)
            {
                bossHP.gameObject.SetActive(false);
                GameController _gameController =  GameObject.Find("GameController").GetComponent<GameController>();
                _gameController._score += score;
                _gameController.Clear();
                Destroy(this.gameObject);
            }
            else if(skillData[skillCount - 1][0] == 1)
            {
                CircleBullet();
            }
            else if(skillData[skillCount - 1][0] == 2)
            {
                HexagramBullet();
            }
            else if(skillData[skillCount - 1][0] == 3)
            {
                OverCircleBullet();
            }
            bossHealth = maxHealth;
        }
        if(frameCount == interval)
        {
            BulletAngle = (float)Math.Atan2(this.transform.position.y - _player.transform.position.y , this.transform.position.x - _player.transform.position.x) * 180f / (float)Math.PI;
            Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 90f));
            Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 97f));
            Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 83f));
            Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 104f));
            Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 76f));
            frameCount = 0;
        }
        if(Time.time - StartTime >= timeSpan)
        {
            StartTime = Time.time;
            MoveAngle += 360f / rotationCount;
        }
        this.transform.position += new Vector3((float)Math.Sin(MoveAngle * Math.PI / 180) * speed * Time.deltaTime , (float)Math.Cos(MoveAngle * Math.PI / 180) * speed * Time.deltaTime , 0);

        bossHP.value = bossHealth / maxHealth;
    }
    void CircleBullet()
    {
        float radius = skillData[skillCount - 1][1] ;
        for(float x = -radius; x < radius;)
        {
            float y = (float)Math.Sqrt(radius * radius - x * x) + this.transform.position.y;
            float angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
            Instantiate(Bullet , new Vector3(x + this.transform.position.x , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));

            y = -(float)Math.Sqrt(radius * radius - x * x) + this.transform.position.y;
            angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
            Instantiate(Bullet , new Vector3(x + this.transform.position.x , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));

            x += 0.5f;
        }
        skillData[skillCount - 1][2]--;
        skillData[skillCount - 1][1] += skillData[skillCount - 1][3];

        if(skillData[skillCount - 1][2] != 0) Invoke("CircleBullet" , 0.5f);
        else
        {
            skillCount--;
        }
    }
    void HexagramBullet()
    {
        float radius = skillData[skillCount - 1][1];
        for(float x = 0;x < (int)radius * Math.Sqrt(3);)
        {
            float y = (float)Math.Sqrt(3) * x - 2f;
            float angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
            Instantiate(Bullet , new Vector3(x , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
            y = -(float)Math.Sqrt(3) * x + 2;
            angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
            Instantiate(Bullet , new Vector3(x , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
            y = 1f;
            angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
            Instantiate(Bullet , new Vector3(x , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
            y = -1f;
            angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
            Instantiate(Bullet , new Vector3(x , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
            if(x != 0f)
            {
                float X = -x;
                y = -(float)Math.Sqrt(3) * X + 2f;
                angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
                Instantiate(Bullet , new Vector3(X , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
                y = (float)Math.Sqrt(3) * X + 2;
                angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
                Instantiate(Bullet , new Vector3(X , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
                y = 1f;
                angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
                Instantiate(Bullet , new Vector3(X , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
                y = -1f;
                angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
                Instantiate(Bullet , new Vector3(X , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
            }
            x += 0.5f;
        }
        skillCount--;
    }
    void OverCircleBullet()
    {
        float radius = skillData[skillCount - 1][1];
        for(float x = -10f; x <= 10f;)
        {
            float y = (float)Math.Sqrt(radius * radius - x * x) - 17f;
            float angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
            Instantiate(Bullet , new Vector3(x, y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
            x += 0.25f;
        }
        skillCount--;
    }
    void HPcolorChange(int count)
    {
        float RGB = 255f;
        switch(count)
        {
            case 1:
                bossHP.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color
                    = new Color(255f / RGB , 0f / RGB , 0f / RGB);
                bossHP.transform.GetChild(0).GetComponent<Image>().color = new Color(155f / RGB , 155f / RGB , 155f / RGB);
                break;
            case 2:
                bossHP.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color
                    = new Color(255f / RGB , 140f / RGB , 0f / RGB);
                bossHP.transform.GetChild(0).GetComponent<Image>().color = Color.red;
                break;
            case 3:
                bossHP.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color
                    = new Color(0f / RGB , 255f / RGB , 0f / RGB);
                bossHP.transform.GetChild(0).GetComponent<Image>().color = new Color(255f / RGB , 140f / RGB , 0f / RGB);
                break;
            case 4:
                bossHP.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color
                    = new Color(0f / RGB , 191f / RGB , 255f / RGB);
                bossHP.transform.GetChild(0).GetComponent<Image>().color = new Color(0f / RGB , 255f / RGB , 0f / RGB);
                break;
            case 5:
                bossHP.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color
                    = new Color(0f / RGB , 0f / RGB , 255f / RGB);
                bossHP.transform.GetChild(0).GetComponent<Image>().color = new Color(0f / RGB , 191f / RGB , 255f / RGB);
                break;
            case 6:
                bossHP.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color
                    = new Color(255f / RGB , 0f / RGB , 255f / RGB);
                bossHP.transform.GetChild(0).GetComponent<Image>().color = new Color(0f / RGB , 0f / RGB , 255f / RGB);
                break;
            case 7:
                bossHP.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color
                    = new Color(0f / RGB , 0f / RGB , 0f / RGB);
                bossHP.transform.GetChild(0).GetComponent<Image>().color = new Color(255f / RGB , 0f / RGB , 255f / RGB);
                break;
        }
    }
}
