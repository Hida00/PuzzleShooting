using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BulletBomb : MonoBehaviour
{
    public GameObject bullet;
    public Vector3 explosionPos;
    public Image image;

    GameObject canvas;
    Camera MainCamera;
    Image bombImage;

    public float damage = 10f;
    float time;
    float speed;

    public int abs;
    int count = 0;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        this.transform.position = Vector3.zero;
        time = Time.time + 0.01f;
        speed = 0.8f;

        bombImage = Instantiate(image , canvas.transform);
        bombImage.sprite = Resources.Load<Sprite>(@"Image/other/bomb");
        bombImage.transform.localScale *= 0.5f;
    }

    void Update()
    {
        bombImage.transform.position =
            RectTransformUtility.WorldToScreenPoint(MainCamera , this.transform.position);

        if(Vector3.Distance(this.transform.position , explosionPos) > 0.1f)
        {
            float _time = speed / (Time.time - time);
            float angle = Mathf.Atan2(explosionPos.y - this.transform.position.y , explosionPos.x - this.transform.position.x);
            this.transform.rotation = Quaternion.Euler(0 , 0 , angle * Mathf.Rad2Deg + 90f);
            this.transform.position += new Vector3(Mathf.Cos(angle) , Mathf.Sin(angle) , 0) * Time.deltaTime * _time;
        }
        else
        {
            if(count % 12 == 0)
            {
                var obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 0));
                obj.GetComponent<CircleBullet>().rotate = 0;
                obj.GetComponent<CircleBullet>().abs = abs;
                obj.GetComponent<CircleBullet>().damagePoint = damage;
                obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 45));
                obj.GetComponent<CircleBullet>().rotate = 45;
                obj.GetComponent<CircleBullet>().abs = abs;
                obj.GetComponent<CircleBullet>().damagePoint = damage;
                obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 90));
                obj.GetComponent<CircleBullet>().rotate = 90;
                obj.GetComponent<CircleBullet>().abs = abs;
                obj.GetComponent<CircleBullet>().damagePoint = damage;
                obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 135));
                obj.GetComponent<CircleBullet>().rotate = 135;
                obj.GetComponent<CircleBullet>().abs = abs;
                obj.GetComponent<CircleBullet>().damagePoint = damage;
                obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 180));
                obj.GetComponent<CircleBullet>().rotate = 180;
                obj.GetComponent<CircleBullet>().abs = abs;
                obj.GetComponent<CircleBullet>().damagePoint = damage;
                obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 225));
                obj.GetComponent<CircleBullet>().rotate = 225;
                obj.GetComponent<CircleBullet>().abs = abs;
                obj.GetComponent<CircleBullet>().damagePoint = damage;
                obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 270));
                obj.GetComponent<CircleBullet>().rotate = 270;
                obj.GetComponent<CircleBullet>().abs = abs;
                obj.GetComponent<CircleBullet>().damagePoint = damage;
                obj = Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 315));
                obj.GetComponent<CircleBullet>().rotate = 315;
                obj.GetComponent<CircleBullet>().abs = abs;
                obj.GetComponent<CircleBullet>().damagePoint = damage;
            }
            if(count >= 60)
            {
                Destroy(bombImage);
                Destroy(this.gameObject);
            }
            count++;
        }
    }
}
