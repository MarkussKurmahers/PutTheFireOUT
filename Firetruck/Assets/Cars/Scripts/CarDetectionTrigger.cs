using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDetectionTrigger : MonoBehaviour
{
    public BasicCarAI carai;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car") )
        {
            carai.remap();
        }
    }
}
