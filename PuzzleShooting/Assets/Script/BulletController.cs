using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    PlayerController _playerController;
    PanelController _panelController;

    public Image bulletImage;
    Image img;
    GameObject canvas;

    public Vector2 scale = new Vector2(0.2f , 0.8f);

    public string imageName = "bullet";

    public float speed;// = 0.1f;
    public float damagePoint = 1.0f;
    float moveOverY;

    public bool isBoss = false;
    public bool isPlayer;
    public bool isTracking; 

    void Start()
    {
        canvas = GameObject.Find("BulletPanel");
        img = Instantiate(bulletImage , canvas.transform);
        img.GetComponent<Image>().sprite = Resources.Load<Sprite>(@"Image/other/" + imageName);
        img.rectTransform.localScale = scale;
        img.rectTransform.rotation = this.transform.rotation;
        img.transform.position
            = RectTransformUtility.WorldToScreenPoint(Camera.main , this.transform.position);

        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
        moveOverY = (19.2f * ((float)Screen.height / (float)Screen.width)) + 1f;
    }

    void Update()
    {
        img.transform.position
            = RectTransformUtility.WorldToScreenPoint(Camera.main , this.transform.position);
        img.rectTransform.rotation = this.transform.rotation;

        float skill = _panelController.skillSpeed;

        if(isTracking)
        {
            if(isPlayer)
            {
                this.transform.position += transform.up * speed * Time.deltaTime * 0.7f * skill;
                try
                {
                    var Enemy = GameObject.FindGameObjectsWithTag("ENEMY");
                    float angle = (float)Math.Atan2(this.transform.position.y - Enemy[0].transform.position.y , this.transform.position.x - Enemy[0].transform.position.x) * 180f / (float)Math.PI;
                    this.transform.rotation = Quaternion.Euler(0 , 0 , angle + 90f);
                    //this.transform.position += new Vector3((float)Math.Sin(angle) * speed * Time.deltaTime , (float)Math.Cos(angle) * speed * Time.deltaTime , 0) * 0.7f;
                    this.transform.position += transform.up * speed * Time.deltaTime * 0.5f * skill;
                }
                catch
                {
                    this.transform.position += transform.up * speed * Time.deltaTime * 0.7f * skill;
                }
            }
        }
        else
        {
            if(isPlayer)
            {
                this.transform.position += transform.up * speed * Time.deltaTime * 0.6f * skill;
            }
            else
            {
                if(!isBoss) this.transform.position += transform.up * speed * Time.deltaTime * 0.3f * skill;
            }
        }

        if (this.transform.position.y >= moveOverY || this.transform.position.y <= -moveOverY || this.transform.position.x <= -13f || this.transform.position.x >= 13f)
        {
            Destroy(this.gameObject);
            Destroy(img);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PLAYER") && !isPlayer)
        {
            //ここにプレイヤーが弾に当たった時の処理を書く
            //Debug.Log("Hit!");
            _playerController.health_Point -= damagePoint - _playerController.defence;
            Destroy(this.gameObject);
            Destroy(img);
        }
        if(other.gameObject.CompareTag("ENEMY") && isPlayer)
        {
            //Debug.Log("Hit");
            float defense = other.gameObject.GetComponent<Viran>().defensePoint;
            other.gameObject.GetComponent<Viran>().ViranHealth -= (damagePoint - defense);
            Destroy(this.gameObject);
            Destroy(img);
        }
        if(other.gameObject.CompareTag("BOSS") && isPlayer)
        {
            float defence = other.gameObject.GetComponent<Boss>().defensePoint;
            other.gameObject.GetComponent<Boss>().bossHealth -= (damagePoint - defence);
            Destroy(this.gameObject);
            Destroy(img);
        }
        if(other.gameObject.CompareTag("MIDBOSS") && isPlayer)
        {
            float defence = other.gameObject.GetComponent<MidBoss>().defencePoint;
            other.gameObject.GetComponent<MidBoss>().HealthPoint -= (damagePoint - defence);
            Destroy(this.gameObject);
            Destroy(img);
        }
        if(other.gameObject.CompareTag("LASTBOSS") && isPlayer)
        {
            float defence = other.gameObject.GetComponent<LastBoss>().defencePoint;
            other.gameObject.GetComponent<LastBoss>().HealthPoint -= ( damagePoint - defence );
            Destroy(this.gameObject);
            Destroy(img);
        }
    }
}

