using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    public GameObject pointer;
    public GameObject lazer;

    PanelController _panelController;

    GameObject player;
    GameObject[] pointers;

    Vector3 P_Pos;

    public float damage;
    public float radius = 5f;
    float time;

    int[] nums;
    int count = 0;
    
    void Start()
    {
        nums = new int[6] { 0 , 1 , 2 , 3 , 4 , 5 };
        pointers = new GameObject[6];

        player = GameObject.Find("Player");

        System.Random rng = new System.Random();
        int n = nums.Length;
        while(n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int tmp = nums[k];
            nums[k] = nums[n];
            nums[n] = tmp;
        }

        P_Pos = player.transform.position;
        foreach(var x in nums)
		{
            pointers[x] = Instantiate(pointer , P_Pos + (new Vector3(Mathf.Cos(60 * x * Mathf.Deg2Rad) , Mathf.Sin(60 * x * Mathf.Deg2Rad) , 0)) * radius , Quaternion.identity);
		}

        _panelController = GameObject.Find("PanelController").GetComponent<PanelController>();

        time = Time.time;
        Invoke(nameof(Finish) , 1.1f);
    }

    void Update()
    {
        if(count < 6 && (Time.time - time - 0.5f) >= 0.10f * count && !_panelController.isSkill && !_panelController.isPause)
		{
            Vector3 pos = pointers[count].transform.position;
            float angle = Mathf.Atan2(pos.y - P_Pos.y , pos.x - P_Pos.x) * Mathf.Rad2Deg;
            var obj = Instantiate(lazer , pos , Quaternion.Euler(0 , 0 , angle + 90f));
            obj.GetComponent<Lazer>().damagePoint = damage;
            obj.GetComponent<Lazer>().speed = 10f;
            count++;
		}
    }

    void Finish()
	{
        foreach(var x in pointers)
		{
            Destroy(x.GetComponent<Pointer>().img);
            Destroy(x);
		}
        Destroy(this.gameObject);
	}
}
