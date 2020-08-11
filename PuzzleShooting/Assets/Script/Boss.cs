using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject Bullet;
    public ParticleSystem particle;
    Slider bossHP;
    GameObject _player;

    PanelController _panelController;

    Vector3 startPos;

    public float MoveAngle;
    public float BulletAngle;
    public float rotationCount;
    public float bossHealth = 500f;
    public float timeSpan;
    public float speed;
    public float skillInterval;
    public float defensePoint = 10f;
    public float StartTime;
    public float difTime;
    float maxHealth;
    float move = 1f;
    float Angle;

    public List<float[]> skillData = new List<float[]>();

    public int interval;
    public int skillCount;
    public int score;
    int frameCount = 0;

    void Start()
    {
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
        _player = GameObject.Find("Player");
        bossHP = GameObject.Find("bossHP").GetComponent<Slider>();
        StartTime = Time.time;
        maxHealth = bossHealth;
        HPcolorChange(skillCount);

        startPos = this.transform.position;
        Angle = MoveAngle;
    }

    void Update()
    {
        if(!_panelController.isSkill)frameCount++;
        if(bossHealth <= 0f && !_panelController.isSkill)
        {
            HPcolorChange(skillCount);
            if(skillCount == 0)
            {
                bossHP.gameObject.SetActive(false);
                GameController _gameController =  GameObject.Find("GameController").GetComponent<GameController>();
                _gameController._score += score;
                _gameController.Clear();
                Instantiate(particle,this.transform.position,Quaternion.Euler(90 , 0 , 0));
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
        if(frameCount == interval && !_panelController.isSkill)
        {
            BulletAngle = (float)Math.Atan2(this.transform.position.y - _player.transform.position.y , this.transform.position.x - _player.transform.position.x) * 180f / (float)Math.PI;
            Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 90f));
            Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 97f));
            Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 83f));
            Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 104f));
            Instantiate(Bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 76f));
            frameCount = 0;
        }
        if(Time.time - StartTime >= timeSpan && !_panelController.isSkill)
        {
            StartTime = Time.time;
            MoveAngle += 360f / rotationCount;
        }
        float skill = _panelController.skillSpeed;
        this.transform.position += new Vector3((float)Math.Sin(MoveAngle * Math.PI / 180) * speed * Time.deltaTime , (float)Math.Cos(MoveAngle * Math.PI / 180) * speed * Time.deltaTime , 0) * move * skill;

        bossHP.value = bossHealth / maxHealth;
    }
    void CircleBullet()     //Num,半径,円の数,各円の半径の差
    {
        move = 0f;
        this.transform.position = startPos;
        MoveAngle = Angle;

        float radius = skillData[skillCount - 1][1] ;
        for(float x = -radius; x < radius;)
        {
            float y = (float)Math.Sqrt(radius * radius - x * x) + this.transform.position.y;
            float angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
            var obj = Instantiate(Bullet , new Vector3(x + this.transform.position.x , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
            obj.GetComponent<BulletController>().isBoss = true;

            y = -(float)Math.Sqrt(radius * radius - x * x) + this.transform.position.y;
            angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
            obj = Instantiate(Bullet , new Vector3(x + this.transform.position.x , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
            obj.GetComponent<BulletController>().isBoss = true;

            x += 0.5f;
        }
        skillData[skillCount - 1][2]--;
        skillData[skillCount - 1][1] += skillData[skillCount - 1][3];

        if(skillData[skillCount - 1][2] != 0) Invoke("CircleBullet" , 0.5f);
        else
        {
            skillCount--;
            move = 1f;
            StartTime = Time.time;
            GameObject.Find("GameController").GetComponent<GameController>().BossBulletMoveStart();
        }
    }
    void HexagramBullet()   //Num,半径
    {
        move = 0f;
        this.transform.position = startPos;
        MoveAngle = Angle;

        float radius = skillData[skillCount - 1][1];
        for(float x = 0;x < (int)radius * Math.Sqrt(3);)
        {
            float y = (float)Math.Sqrt(3) * x - 2f;
            float angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
            var obj = Instantiate(Bullet , new Vector3(x , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
            obj.GetComponent<BulletController>().isBoss = true;
            y = -(float)Math.Sqrt(3) * x + 2;
            angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
            obj = Instantiate(Bullet , new Vector3(x , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
            obj.GetComponent<BulletController>().isBoss = true;
            y = 1f;
            angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
            obj = Instantiate(Bullet , new Vector3(x , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
            obj.GetComponent<BulletController>().isBoss = true;
            y = -1f;
            angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
            obj = Instantiate(Bullet , new Vector3(x , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
            obj.GetComponent<BulletController>().isBoss = true;
            if(x != 0f)
            {
                float X = -x;
                y = -(float)Math.Sqrt(3) * X + 2f;
                angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
                obj = Instantiate(Bullet , new Vector3(X , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
                obj.GetComponent<BulletController>().isBoss = true;
                y = (float)Math.Sqrt(3) * X + 2;
                angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
                obj = Instantiate(Bullet , new Vector3(X , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
                obj.GetComponent<BulletController>().isBoss = true;
                y = 1f;
                angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
                obj = Instantiate(Bullet , new Vector3(X , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
                obj.GetComponent<BulletController>().isBoss = true;
                y = -1f;
                angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
                obj = Instantiate(Bullet , new Vector3(X , y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
                obj.GetComponent<BulletController>().isBoss = true;
            }
            x += 0.25f;
        }
        skillCount--;
        move = 1f;
        StartTime = Time.time;
        GameObject.Find("GameController").GetComponent<GameController>().BossBulletMoveStart();
    }
    void OverCircleBullet()//Num,半径,円の数,半径の差,弾の間隔,生成する弾のX座標の最小値,生成する弾のX座標の最大値,time,
    {
        move = 0f;
        this.transform.position = startPos;
        MoveAngle = Angle;

        float radius = skillData[skillCount - 1][1];
        for(float x = skillData[skillCount - 1][5]; x <= skillData[skillCount - 1][6];)
        {
            float y = (float)Math.Sqrt(radius * radius - x * x) - 17f;
            float angle = (float)Math.Atan2(y - _player.transform.position.y , x - _player.transform.position.x) * 180f / (float)Math.PI;
            var obj = Instantiate(Bullet , new Vector3(x, y , 0) , Quaternion.Euler(0 , 0 , 90f + angle));
            obj.GetComponent<BulletController>().isBoss = true;
            x += skillData[skillCount - 1][4];
        }
        skillData[skillCount - 1][2]--;
        skillData[skillCount - 1][1] += skillData[skillCount - 1][3];

        if(skillData[skillCount - 1][2] != 0) Invoke("OverCircleBullet" , 0.5f);
        else
        {
            skillCount--;
            move = 1f;
            StartTime = Time.time;
            GameObject.Find("GameController").GetComponent<GameController>().BossBulletMoveStart();
        }
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
