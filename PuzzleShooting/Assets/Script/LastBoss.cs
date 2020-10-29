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
    public GameObject bomb;
    public GameObject lockOn;
    public GameObject Wall;
    public GameObject bulletPlacer;
    public GameObject wave;
    public GameObject knife;

    Image bossImage;
    GameObject canvas;
    GameObject[] placers;
    Camera MainCamera;

    PanelController _panelController;

    StringReader st;

    public float damage = 10f;
    public float defencePoint = 10;
    public float HealthPoint = 10;
    float startTime;
    float maxHealth;

    public int bulletSpan = 35;
    int bulletCount = 0;
    int score;

    string[] readValues;

    bool canRead = true;
    bool isBullet = true;

    void Start()
    {
        canvas = GameObject.Find("Canvas");

        bossImage = Instantiate(image , canvas.transform);
        bossImage.sprite = Resources.Load<Sprite>(@"Image/Enemy/boss_last");

        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();

        this.transform.localScale *= 3.5f;
        bossImage.transform.localScale *= 3.5f;

        var csv = Resources.Load(@"CSV/StageData/last") as TextAsset;
        st = new StringReader(csv.text);
        string[] values = st.ReadLine().Split(',');

        this.transform.position = new Vector3(0 , float.Parse(values[4]) , 0);
        this.HealthPoint = float.Parse(values[5]);
        this.maxHealth = HealthPoint;
        this.score = int.Parse(values[6]);

        //GameObject.Find("bossHP").SetActive(true);

        startTime = Time.time;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
		{

		}
        bulletCount++;

        bossImage.transform.position =
            RectTransformUtility.WorldToScreenPoint(MainCamera , this.transform.position);

        if(canRead)
        {
            readValues = st.ReadLine().Split(',');
            canRead = false;
        }
        if(readValues[0] == "0" && HealthPoint <= maxHealth * float.Parse(readValues[1]) && !_panelController.isSkill && !_panelController.isPause)
        {
            string[] value = readValues[2].Split(':');
            var obj = Instantiate(bomb , this.transform.position , Quaternion.identity);
            obj.GetComponent<BulletBomb>().explosionPos = new Vector2(float.Parse(value[0]) , float.Parse(value[1]));
            obj.GetComponent<BulletBomb>().abs = int.Parse(readValues[3]);
            canRead = true;
        }
        if(readValues[0] == "1" && HealthPoint <= maxHealth * float.Parse(readValues[1]) && !_panelController.isSkill && !_panelController.isPause)
        {
            var obj = Instantiate(lockOn);
            obj.GetComponent<LockOn>().damage = float.Parse(readValues[2]);
            obj.GetComponent<LockOn>().radius = float.Parse(readValues[3]);
            canRead = true;
		}
        if(readValues[0] == "2" && HealthPoint <= maxHealth * float.Parse(readValues[1]) && !_panelController.isSkill && !_panelController.isPause)
        {
            var obj = Instantiate(Wall);
            obj.GetComponent<Wall>().damage = float.Parse(readValues[2]);
            obj.GetComponent<Wall>().XPos = float.Parse(readValues[3]);
            canRead = true;
        }
        if((readValues[0] == "3") && HealthPoint <= maxHealth * float.Parse(readValues[1]) && !_panelController.isSkill && !_panelController.isPause)
		{
            float[] data = new float[] { float.Parse(readValues[6]) , float.Parse(readValues[7]) , float.Parse(readValues[8]) , float.Parse(readValues[9]), float.Parse(readValues[10]) };
            var obj = Instantiate(bulletPlacer);
            obj.GetComponent<BulletPlacer>().skillData = data;
            obj.GetComponent<BulletPlacer>().dif = float.Parse(readValues[2]);
            obj.GetComponent<BulletPlacer>().speed = float.Parse(readValues[3]);
            obj.GetComponent<BulletPlacer>().damage = float.Parse(readValues[4]);
            obj.GetComponent<BulletPlacer>().interval = int.Parse(readValues[5]);
            obj.GetComponent<BulletPlacer>()._boss = this.gameObject;
            canRead = true;

        }
        if(readValues[0] == "4" && HealthPoint <= maxHealth * float.Parse(readValues[1]) && !_panelController.isSkill && !_panelController.isPause)
        {
            var obj = Instantiate(wave);
            obj.GetComponent<Wave>().posY = new float[] { float.Parse(readValues[2]) , 0 , -float.Parse(readValues[2]) };
            obj.GetComponent<Wave>().damage = float.Parse(readValues[3]);
            obj.GetComponent<Wave>().interval = int.Parse(readValues[4]);
            canRead = true;
        }
        if(readValues[0] == "5" && HealthPoint <= maxHealth * float.Parse(readValues[1]) && !_panelController.isSkill && !_panelController.isPause)
        {
            var obj = Instantiate(knife);
            obj.GetComponent<KnifeController>().speed = float.Parse(readValues[2]);
            obj.GetComponent<KnifeController>().damage = float.Parse(readValues[3]);
            canRead = true;
		}
        if(readValues[0] == "10" && HealthPoint <= maxHealth * float.Parse(readValues[1]) && !_panelController.isSkill && !_panelController.isPause)
		{
            isBullet = !isBullet;
            canRead = true;
		}
        if(bulletCount >= bulletSpan && isBullet && !_panelController.isSkill && !_panelController.isPause)
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
            obj = Instantiate(bullet , this.transform.position + Vector3.right * 4 , Quaternion.Euler(0 , 0 , 200));
            obj.GetComponent<BulletController>().damagePoint = damage * 0.7f;
            obj.GetComponent<BulletController>().bulletImage = ColorBullets[1];
            obj = Instantiate(bullet , this.transform.position + Vector3.left * 4 , Quaternion.Euler(0 , 0 , 160));
            obj.GetComponent<BulletController>().damagePoint = damage * 0.7f;
            obj.GetComponent<BulletController>().bulletImage = ColorBullets[1];
            bulletCount = 0;
        }
        else if(!isBullet)
		{
            bulletCount = 0;
		}

        if(HealthPoint <= 0)
        {
            GameController _gameController = GameObject.Find("GameController").GetComponent<GameController>();
            _gameController._score += score;
            _gameController.FinishGame(true);
            _gameController.Clear();

            Destroy(bossImage);
            Destroy(this.gameObject);
        }
    }
}
