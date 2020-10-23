using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleBullet : MonoBehaviour
{
    PlayerController _playerController;

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
        moveOverY = ( 19.2f * ( (float)Screen.height / (float)Screen.width ) ) + 1f;
    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PLAYER") && !isPlayer)
        {
            _playerController.health_Point -= damagePoint - _playerController.defence;
            Destroy(this.gameObject);
            Destroy(img);
        }
    }
}
