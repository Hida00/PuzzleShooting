using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MidBoss : MonoBehaviour
{
    public GameObject bullet;
    public Image image;
    Image img;
    public ParticleSystem particle;

    PanelController _panelController;
    GameObject canvas;

    public Vector3 scale;

    public float TimeSpan;
    public float HealthPoint;
    public float defencePoint = -3.5f;
    public float damage;
    public float imgscale;
    float angle = -12.5f;
    float startTime;
    float Y;

    public int interval;
    public int score;
    public int isFinal;
    int frameCount = 0;

    public string imageName;

    void Start()
    {
        Y = 19.2f * (Screen.height / Screen.width);

        canvas = GameObject.Find("Canvas");
        img = Instantiate(image , canvas.transform);
        img.sprite = Resources.Load<Sprite>(@"Image/Enemy/" + imageName);
        img.rectTransform.position
            = RectTransformUtility.WorldToScreenPoint(Camera.main , this.transform.position);
        img.rectTransform.sizeDelta *= imgscale;

        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
        startTime = Time.time;
    }

    void Update()
    {
        img.rectTransform.position
            = RectTransformUtility.WorldToScreenPoint(Camera.main , this.transform.position);

        if(!_panelController.isSkill) frameCount++;
        if(Time.time - startTime >= TimeSpan)
        {
            Vector3 pos = GameObject.Find("Player").transform.position;
            float angle = (float)(Math.Atan2(this.transform.position.y - pos.y , this.transform.position.x - pos.x) * 180f / Math.PI);

            var obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 90f + angle));
            obj.transform.localScale = scale;
            obj.GetComponent<BulletController>().speed *= 1.5f;
            obj.GetComponent<BulletController>().damagePoint = damage * 2.3f;
            obj.GetComponent<BulletController>().imageName = "bullet2";
            obj.GetComponent<BulletController>().scale = Vector2.one;
            startTime = Time.time;
        }
        if(frameCount == interval && !_panelController.isSkill)
        {
            var obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 180f + angle));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 155f + angle));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 205f + angle));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 130f + angle));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 230f + angle));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 105f + angle));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 255f + angle));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 ,  80f + angle));
            obj.GetComponent<BulletController>().damagePoint = damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 280f + angle));
            obj.GetComponent<BulletController>().damagePoint = damage;

            angle += 3.333f;
            if(angle >= 12.5f)
            {
                angle = -12.5f + (angle - 12.5f);
            }

            frameCount = 0;
        }
        if(HealthPoint <= 0f && !_panelController.isSkill)
        {
            GameObject.Find("Generator").GetComponent<Generator>().Ecount++;
            Instantiate(particle , this.transform.position , Quaternion.Euler(90 , 0 , 0));
            Destroy(img);
            Destroy(this.gameObject);
        }
    }
}
