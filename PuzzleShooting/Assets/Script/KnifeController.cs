using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeController : MonoBehaviour
{

    public GameObject bullet;
    public Image image;

    public float speed;
    public float damage;

    void Start()
    {
        Vector3 player = GameObject.Find("Player").transform.position;
		var obj = Instantiate(bullet , player + Vector3.up , Quaternion.Euler(0 , 0 , 180));
		obj.GetComponent<BulletController>().bulletImage = image;
		obj.GetComponent<BulletController>().damagePoint = damage;
		obj.GetComponent<BulletController>().speed = speed;
		obj.GetComponent<BulletController>().scale *= 3.5f;

		Destroy(this.gameObject);
    }
}
