using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class haiti : MonoBehaviour
{
    // Start is called before the first frame update
    TextAsset csvFile;
    List<string[]> csvDatas = new List<string[]>();



    GameObject viran;

    float span =2.0f;
    float delta = 0;


    // Start is called before the first frame update
    void Start()
    {
        
      

        csvFile = Resources.Load("Book1") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }



        float x = float.Parse(csvDatas[0][0]);
        float x2 = float.Parse(csvDatas[0][1]);
        float y = float.Parse(csvDatas[0][2]);

        float a=UnityEngine.Random.Range(x2, x);

        transform.position = new Vector2(a, y);


        viran = GameObject.Find("viran");
         

    }



    // Update is called once per frame
    void Update()

    {
        transform.Translate(0.1f, 0, 0);
    
        if (transform.position.x > 5.0f)
        {
            transform.Rotate(0,0,180);
            
        }

        if (transform.position.x < -5.0f)
        {
            transform.Rotate(0, 0, 180);
        }

        delta += Time.deltaTime;
        if (delta > span)
        {
            Destroy(gameObject);
        }
    }
}


