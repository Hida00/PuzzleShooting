using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject bullet;

    PanelController _panelController;

    Quaternion up;
    Quaternion down;

    public float damage;
    public float[] posY = new float[3] { 5 , 0 , -5 };
    float posX;

    public int interval = 25;
    int count = 0;

    void Start()
    {
        posX = 9.6f;
        up = Quaternion.Euler(0 , 0 , 0);
        down = Quaternion.Euler(0 , 0 , -180);

        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
    }

    void Update()
    {
        if(count >= interval && !_panelController.isSkill)
		{
            var obj = Instantiate(bullet , new Vector3(posX , posY[0]) , up);
            obj.GetComponent<WaveBullet>().damagePoint = damage;
            obj.GetComponent<WaveBullet>().speed = 3;
            obj.GetComponent<WaveBullet>().abs = 1;
            obj = Instantiate(bullet , new Vector3(posX , posY[0]) , down);
            obj.GetComponent<WaveBullet>().damagePoint = damage;
            obj.GetComponent<WaveBullet>().speed = 3;
            obj.GetComponent<WaveBullet>().abs = -1;

            obj = Instantiate(bullet , new Vector3(-posX , posY[1]) , up);
            obj.GetComponent<WaveBullet>().damagePoint = damage;
            obj.GetComponent<WaveBullet>().speed = 3;
            obj.GetComponent<WaveBullet>().abs = 1;
            obj = Instantiate(bullet , new Vector3(-posX , posY[1]) , down);
            obj.GetComponent<WaveBullet>().damagePoint = damage;
            obj.GetComponent<WaveBullet>().speed = 3;
            obj.GetComponent<WaveBullet>().abs = -1;

            obj = Instantiate(bullet , new Vector3(posX , posY[2]) , up);
            obj.GetComponent<WaveBullet>().damagePoint = damage;
            obj.GetComponent<WaveBullet>().speed = 3;
            obj.GetComponent<WaveBullet>().abs = 1;
            obj = Instantiate(bullet , new Vector3(posX , posY[2]) , down);
            obj.GetComponent<WaveBullet>().damagePoint = damage;
            obj.GetComponent<WaveBullet>().speed = 3;
            obj.GetComponent<WaveBullet>().abs = -1;
            count = 0;
        }

        posX += -0.1f;
        count++;

        if(Mathf.Abs(posX) > 9.6f)
		{
            Destroy(this.gameObject);
		}
    }
}
