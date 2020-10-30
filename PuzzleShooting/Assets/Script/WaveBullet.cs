using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBullet : MonoBehaviour
{
    PlayerController _playerController;
    PanelController _panelController;

    public Image bulletImage;
    Image img;
    GameObject canvas;

    public Vector2 scale = new Vector2(0.2f , 0.8f);

    public string imageName = "lazer";

    public float speed;
    public float damagePoint = 1.0f;
    public float rotate = 0;
    float moveOverY;
    float pos;

    public int abs = 1;

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
        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();
        moveOverY = (19.2f * ((float)Screen.height / (float)Screen.width)) + 1f;

        this.transform.localScale = scale;
        pos = this.transform.position.x;
    }

    void Update()
    {
        img.transform.position
            = RectTransformUtility.WorldToScreenPoint(Camera.main , this.transform.position);
        img.rectTransform.rotation = this.transform.rotation;

        float skill = _panelController.skillSpeed;

        this.transform.position = new Vector3(pos + Mathf.Sin(this.transform.position.y) , this.transform.position.y , 0);
		this.transform.position += Vector3.up * speed * Time.deltaTime * abs * skill;

		if(this.transform.position.y >= moveOverY || this.transform.position.y <= -moveOverY || this.transform.position.x <= -13f || this.transform.position.x >= 13f)
        {
            Destroy(this.gameObject);
            Destroy(img);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.CompareTag("PLAYER") && !isPlayer)
		{
			_playerController.health_Point -= damagePoint - _playerController.defence;
			Destroy(this.gameObject);
			Destroy(img);
		}
	}
}
