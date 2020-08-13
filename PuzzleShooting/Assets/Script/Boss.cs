using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Placer;
    public ParticleSystem particle;
    Slider bossHP;
    GameObject _player;
    GameObject[] placer;

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
    float Angle;

    public List<float[]> skillData = new List<float[]>();

    public int interval;
    public int skillCount;
    public int score;
    int frameCount = 0;

    public bool isPlaceFinish;

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
        placer = new GameObject[6];
    }

    void Update()
    {
        if(!_panelController.isSkill)frameCount++;
        if(bossHealth <= 0f && !_panelController.isSkill)
        {
            foreach(var placers in placer) Destroy(placers);
            GameObject.Find("GameController").GetComponent<GameController>().BossBulletMoveStart();
            HPcolorChange(skillCount);
            if(skillCount == 0)
            {
                bossHP.gameObject.SetActive(false);
                GameController _gameController =  GameObject.Find("GameController").GetComponent<GameController>();
                _gameController._score += score;
                _gameController.Clear();
                _gameController.FinishGame(true);
                Instantiate(particle,this.transform.position,Quaternion.Euler(90 , 0 , 0));
                Destroy(this.gameObject);
            }
            else if(skillData[skillCount - 1][0] == 1)
            {
                CircleBullet();
            }
            else if(skillData[skillCount - 1][0] == 2)
            {
                StarBullet();
            }
            else if(skillData[skillCount - 1][0] == 3)
            {
                OverCircleBullet();
            }
            bossHealth = maxHealth;
        }
        if(frameCount == interval && !_panelController.isSkill && !isPlaceFinish)
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

        bossHP.value = bossHealth / maxHealth;
    }
    void CircleBullet()         //Num,半径,円の数,各円の半径の差
    {
        placer[0] = Instantiate(Placer);
        placer[0].GetComponent<BulletPlacer>().skillData = skillData[skillCount - 1];
        placer[0].GetComponent<BulletPlacer>()._boss = this.gameObject;
        placer[0].GetComponent<BulletPlacer>().speed = 2f;
        placer[0].GetComponent<BulletPlacer>().interval = 10;
        placer[0].GetComponent<BulletPlacer>().dif = 0f;

        placer[1] = Instantiate(Placer);
        placer[1].GetComponent<BulletPlacer>().skillData = skillData[skillCount - 1];
        placer[1].GetComponent<BulletPlacer>()._boss = this.gameObject;
        placer[1].GetComponent<BulletPlacer>().speed = 2f;
        placer[1].GetComponent<BulletPlacer>().interval = 10;
        placer[1].GetComponent<BulletPlacer>().dif = -2f;

        isPlaceFinish = true;
        skillCount--;
    }
    void StarBullet()
    {
        placer[0] = Instantiate(Placer);
        placer[0].GetComponent<BulletPlacer>().skillData = skillData[skillCount - 1];
        placer[0].GetComponent<BulletPlacer>()._boss = this.gameObject;
        placer[0].GetComponent<BulletPlacer>().speed = 8f;
        placer[0].GetComponent<BulletPlacer>().interval = 7;

        isPlaceFinish = true;
        skillCount--;
    }
    void OverCircleBullet()     //Num,半径,円の数,半径の差,弾の間隔,生成する弾のX座標の最小値,生成する弾のX座標の最大値,time,
    {
        placer[0] = Instantiate(Placer);
        placer[0].GetComponent<BulletPlacer>().skillData = skillData[skillCount - 1];
        placer[0].GetComponent<BulletPlacer>()._boss = _player;
        placer[0].GetComponent<BulletPlacer>().speed = 4f;
        placer[0].GetComponent<BulletPlacer>().interval = 10;
        placer[0].GetComponent<BulletPlacer>().dif = 0f;

        isPlaceFinish = true;
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
