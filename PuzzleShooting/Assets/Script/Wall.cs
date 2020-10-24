using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject lazer;
    public GameObject bullet;

    Quaternion right;
    Quaternion left;

    public float damage = 3.0f;
    public float XPos = 4.5f;
    float YPos;
    float time;

    int count = 0;

    void Start()
    {
        time = Time.time;
        YPos = 19.2f * ((float)Screen.height / (float)Screen.width);

        right = Quaternion.Euler(0 , 0 , 90);
        left = Quaternion.Euler(0 , 0 , -90);
    }

    void Update()
    {
        if(Time.time - time >= 0.1f * count)
		{
            if(count % 2 == 0)
			{
                var obj = Instantiate(lazer , new Vector3(XPos , YPos) , right);
                obj.GetComponent<Lazer>().damagePoint = damage;
                obj.GetComponent<Lazer>().speed *= 0.7f;
                obj = Instantiate(bullet , new Vector3(-XPos , -YPos) , left);
                obj.GetComponent<BulletController>().damagePoint = damage;
                obj.GetComponent<BulletController>().speed *= 0.9f;
            }
            else
            {
                var obj = Instantiate(bullet , new Vector3(XPos , YPos) , right);
                obj.GetComponent<BulletController>().damagePoint = damage;
                obj.GetComponent<BulletController>().speed *= 0.9f;
                obj = Instantiate(lazer , new Vector3(-XPos , -YPos) , left);
                obj.GetComponent<Lazer>().damagePoint = damage;
                obj.GetComponent<Lazer>().speed *= 0.7f;
            }
            YPos -= 1f;
            count++;
		}
        if(Mathf.Abs(YPos) >= 19.2f * ((float)Screen.height / (float)Screen.width))
		{
            Destroy(this.gameObject);
		}
    }
}
