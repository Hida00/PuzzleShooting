using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattery : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject bullet;
    public Image image;

    Image bossImage;
    GameObject player;
    GameObject canvas;
    private PlayerController _playerController;

    int bulletCount = 0;

    public int num;

    void Start()
    {
        player = GameObject.Find("Player");
        _playerController = player.GetComponent<PlayerController>();
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas = GameObject.Find("Canvas");

        bossImage = Instantiate(image , canvas.transform);

        bossImage.transform.position =
            RectTransformUtility.WorldToScreenPoint(MainCamera , this.transform.position);
        this.transform.position = player.transform.position + new Vector3(2f * num , 0 , 0);
    }

    void Update()
    {
        bulletCount++;

        this.transform.position = player.transform.position + new Vector3(2f * num , 0 , 0);
        bossImage.transform.position =
            RectTransformUtility.WorldToScreenPoint(MainCamera , this.transform.position);

        if (bulletCount >= 20)
        {
            var obj = Instantiate(bullet , this.transform.position , Quaternion.identity);
            obj.GetComponent<BulletController>().damagePoint = _playerController.strength * _playerController.damage;
            obj = Instantiate(bullet , this.transform.position , Quaternion.identity);
            obj.GetComponent<BulletController>().damagePoint = _playerController.strength * _playerController.damage;
            bulletCount = 0;
        }
    }
}
