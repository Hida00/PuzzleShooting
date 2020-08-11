using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBoss : MonoBehaviour
{
    public GameObject bullet;
    public ParticleSystem particle;

    PanelController _panelController;

    public float HealthPoint;
    public float defencePoint = -3.5f;
    float angle = -12.5f;

    public int interval;
    public int score;
    public int isFinal;
    int frameCount = 0;

    void Start()
    {
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
    }

    void Update()
    {
        if(!_panelController.isSkill) frameCount++;
        if(frameCount == interval && !_panelController.isSkill)
        {
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 180f + angle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 155f + angle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 205f + angle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 130f + angle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 230f + angle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 105f + angle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 255f + angle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 ,  80f + angle));
            Instantiate(bullet , this.transform.position , Quaternion.Euler(0 , 0 , 280f + angle));

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
            Destroy(this.gameObject);
        }
    }
}
