using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Viran : MonoBehaviour
{
    public GameObject bullet;
    public Image image;
    Image img;
    public ParticleSystem particle;
    GameController _gameController;
    PanelController _panelController;

    GameObject canvas;

    [NonSerialized]
    public float[] MoveAngles;
    [NonSerialized]
    public float ViranHealth = 100f;
    [NonSerialized]
    public float maxHealth;
    [NonSerialized]
    public float defensePoint = 2f;
    [NonSerialized]
    public float displaceTime;
    [NonSerialized]
    public float changeTime;
    [NonSerialized]
    public float startTime;
    [NonSerialized]
    public float speed = 4f;
    [NonSerialized]
    public float BulletAngle;
    [NonSerialized]
    public float AngleAbs;
    [NonSerialized]
    public float difTime;
    [NonSerialized]
    public float MoveAngle;
    [NonSerialized]
    public float damage;
    float Y;

    [NonSerialized]
    public int interval = 15;
    [NonSerialized]
    public int Type;
    [NonSerialized]
    public int score;
    [NonSerialized]
    public int isFinal;
    int frameCount = 0;
    int count = 0;
    int maxCount;

    [NonSerialized]
    public string imageName;

    void Start()
    {
        Y = 19.2f * ((float)Screen.height / (float)Screen.width);

        canvas = GameObject.Find("Canvas");
        img = Instantiate(image , canvas.transform);
        img.rectTransform.anchoredPosition =
            new Vector2(this.transform.position.x / 20f * 471f , this.transform.position.y / Y * 231.5f);
        img.sprite = Resources.Load<Sprite>(@"Image/Enemy/" + imageName);
        img.rectTransform.sizeDelta *= this.transform.localScale.z;

        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();

        maxHealth = ViranHealth;
        defensePoint = 2f;
        startTime = Time.time;
        maxCount = MoveAngles.Length;
    }
    
    void Update()
    {
        img.rectTransform.anchoredPosition =
            new Vector2(this.transform.position.x / 20f * 471f , this.transform.position.y / Y * 231.5f);

        if(!_panelController.isSkill) frameCount++;
        if(ViranHealth <= 0f)
        {
            GameObject.Find("Generator").GetComponent<Generator>().Ecount++;
            _gameController._score += score;
            Instantiate(particle , this.transform.position , Quaternion.Euler(90 , 0 , 0));
            Destroy(img);
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
            Destroy(img);
            Destroy(this.gameObject);
        }
        if(frameCount == interval && Type == 2 && speed != 0
            && !GameObject.Find("PanelController").GetComponent<PanelController>().isSkill)
        {
            var obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0,0,BulletAngle));
            obj.GetComponent<BulletController>().damagePoint = 1f;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0,0,BulletAngle + 25));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0,0,BulletAngle - 25f));
            obj.GetComponent<BulletController>().damagePoint = damage;
            frameCount = 0;
        }
        if(frameCount == interval && speed == 0 && !_panelController.isSkill)
        {
            frameCount = 0;
            Vector3 pos = GameObject.Find("Player").transform.position;
            float angle = (float)(Math.Atan2(this.transform.position.y - pos.y , this.transform.position.x - pos.x) * 180f / Math.PI);

            var obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 ,  90f + angle));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 115f + angle));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 140f + angle));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 ,  65f + angle));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 ,  40f + angle));
            obj.GetComponent<BulletController>().damagePoint = damage;
        }
        if(frameCount == interval && Type == 1 && speed != 0 && !_panelController.isSkill)
        {
            var obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 20f));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle - 20f));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 40f));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle - 40f));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle + 60f));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , BulletAngle - 60f));
            obj.GetComponent<BulletController>().damagePoint = damage;
            frameCount = 0;
        }
        if(this.transform.position.x >= 13f || this.transform.position.x <= -13f || this.transform.position.y >= 15f || this.transform.position.y <= -15f || count >= maxCount)
        {
            GameObject.Find("Generator").GetComponent<Generator>().Ecount++;
            Destroy(img);
            Destroy(this.gameObject);
        }
        float skill = _panelController.skillSpeed;
        this.transform.position += new Vector3((float)Math.Sin(MoveAngle * Math.PI / 180) * speed * Time.deltaTime , (float)Math.Cos(MoveAngle * Math.PI / 180) * speed * Time.deltaTime , 0) * skill;
    }
}
