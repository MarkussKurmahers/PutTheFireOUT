using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDamage : MonoBehaviour
{
    bool isDamaging;

    public GameObject watersplash;
    PlayerControll controller;
    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Fire"))
        {
            isDamaging = true;
            watersplash.SetActive(true);
            StartCoroutine(damaging(collision.GetComponent<FireStats>()) );
        }
    }

    IEnumerator damaging(FireStats fire)
    {
        while(isDamaging)
        {
            int damage=1;
            if (controller.speed >= controller.maxspeed)
            {
                damage++;
                if (controller.speed > controller.maxspeed)
                {
                    damage += 2;
                }
            }
         

                fire.Damagetaken(damage);
            yield return new WaitForSeconds(.1f);
        }
    }

 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            watersplash.SetActive(false);

            isDamaging = false;

        }
    }
}
