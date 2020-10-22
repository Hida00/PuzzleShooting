using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LastBoss : MonoBehaviour
{
    public Image image;
    public Image[] ColorBullets; //Blue,Red,Yellow
    public GameObject placer;
    public GameObject bullet;
    public GameObject trakingBullet;

    Image bossImage;
    GameObject canvas;
    GameObject[] placers;
    Camera MainCamera;

    StringReader st;

    public float damage = 20f;
    public float defencePoint = 10;
    public float HealthPoint = 100;

    public int bulletSpan = 20;
    int bulletCount = 0;

    string[] readValues;

    bool canRead = true;

    void Start()
    {
        canvas = GameObject.Find("Canvas");

        bossImage = Instantiate(image , canvas.transform);
        bossImage.sprite = Resources.Load<Sprite>(@"Image/Enemy/boss_last");

        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        this.transform.localScale *= 3.5f;
        bossImage.transform.localScale *= 3.5f;

        var csv = Resources.Load(@"CSV/StageData/last") as TextAsset;
        st = new StringReader(csv.text);
        string[] values = st.ReadLine().Split(',');

        this.transform.position = new Vector3(0 , float.Parse(values[3]) , 0);
    }

    void Update()
    {
        bulletCount++;

        bossImage.transform.position =
            RectTransformUtility.WorldToScreenPoint(MainCamera , this.transform.position);

        if(canRead)
        {
            readValues = st.ReadLine().Split(',');
            canRead = false;
        }

        if(bulletCount >= bulletSpan)
        {
            var obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 180));
            obj.GetComponent<BulletController>().damagePoint = damage * 0.7f;
            obj.GetComponent<BulletController>().bulletImage = ColorBullets[1];
            obj = Instantiate(bullet , this.transform.position + Vector3.right * 2 , Quaternion.Euler(0 , 0 , 190));
            obj.GetComponent<BulletController>().damagePoint = damage * 0.7f;
            obj.GetComponent<BulletController>().bulletImage = ColorBullets[1];
            obj = Instantiate(bullet , this.transform.position + Vector3.left * 2 , Quaternion.Euler(0 , 0 , 170));
            obj.GetComponent<BulletController>().damagePoint = damage * 0.7f;
            obj.GetComponent<BulletController>().bulletImage = ColorBullets[1];
            bulletCount = 0;
        }

        if(HealthPoint <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
