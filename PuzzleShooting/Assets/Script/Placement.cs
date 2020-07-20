using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Placement: MonoBehaviour
{
    TextAsset csvFile;

    GameObject viran;

    float span =2.0f;
    float delta = 0;

    void Start()
    {
        Create_Viran();
    }

    void Create_Viran()
    {
        csvFile = Resources.Load(@"CSV/Book1") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            string[] line = reader.ReadLine().Split(',');
            float x = float.Parse(line[0]);
            float x2 = float.Parse(line[1]);
            float y = float.Parse(line[2]);

            Debug.Log($"{line[0]},{line[1]},{line[2]}");

            float a = UnityEngine.Random.Range(x2 , x);

            transform.position = new Vector2(a , y);
        }

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


