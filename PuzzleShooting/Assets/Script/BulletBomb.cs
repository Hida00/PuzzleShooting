using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBomb : MonoBehaviour
{
    public GameObject bullet;
    public Vector3 explosionPos;

    float time;

    void Start()
    {
        this.transform.position = Vector3.zero;
        explosionPos = new Vector3(3 , 3 , 0);
        time = Time.time + 0.01f;
    }

    void Update()
    {
        float _time = 1f / (Time.time - time);
        
        float angle = Mathf.Atan2(explosionPos.y - this.transform.position.y , explosionPos.x - this.transform.position.x) * Mathf.Rad2Deg;
        if (Vector3.Distance(this.transform.position , explosionPos) > 0.1f)
        {
            this.transform.position += new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) , Mathf.Cos(angle * Mathf.Deg2Rad) , 0) * Time.deltaTime * _time;
        }
        else
        {

        }
    }
}
