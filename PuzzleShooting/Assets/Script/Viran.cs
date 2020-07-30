using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viran : MonoBehaviour
{
    public float ViranHealth = 100f;
    public float maxHealth;
    public float defensePoint = 2f;

    void Start()
    {
        maxHealth = ViranHealth;
        defensePoint = 2f;
    }
    
    void Update()
    {
        if(ViranHealth <= 0f)
        {
            Debug.Log("KILL");
            Destroy(this.gameObject);
        }
    }
}
