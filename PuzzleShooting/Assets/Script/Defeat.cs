using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeat : MonoBehaviour
{
    void Start()
    {
        Invoke("Delete" , 1.0f);
    }
    
    void Update()
    {
        
    }

    void Delete()
    {
        Destroy(this.gameObject);
    }
}
