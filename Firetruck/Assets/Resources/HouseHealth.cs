using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HouseHealth : MonoBehaviour
{

    public int HouseHP=0;
    int maxhp;
    public Slider healthslide;
    Coroutine healthregen;
    [SerializeField] PlayerStats playerstats;
    Color housecolor;
    Vector2 fireposition;
    bool repair;
    static bool playerdead;
   

    private void Start()
    {
      
        playerdead = false;
        maxhp = HouseHP;
        healthslide.maxValue = HouseHP;
        healthslide.value = HouseHP;
    }
    public void SaveSpawner(Vector2 pos)
    {
        fireposition = pos;
    }
    IEnumerator deathsequence()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>().accelaration = 0;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<HoseFollow>().enabled = false;

        GameObject.Find("Main Camera").GetComponent<CameraFollow>().target = transform;
      
        yield return new WaitForSeconds(2);
        playerstats.damagePlayer();


        HouseHP = 0;
        Debug.Log(HouseHP);
        healthslide.value = HouseHP;
        healthslide.GetComponent<Animator>().SetTrigger("pop");

        housecolor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.gray;
        repair = true;
        playerstats.damagePlayer();
    }


    public void HouseDamageTaken(int Damage)
    {
        if(playerdead)
        {
            return;
        }


        if (playerstats.getCurrentHealth() == 1 && HouseHP == 1)
        {
            playerdead = true;
            StartCoroutine(deathsequence());
            if (healthregen != null)
            {
                StopCoroutine(healthregen);
            }
            return;

        }

        HouseHP -= Damage;
        healthslide.value = HouseHP;
        healthslide.GetComponent<Animator>().SetTrigger("pop");
        
            if (HouseHP <= 0 )//if the value of fireHealth is less than 0, then the Fire will be Destroyed
            {
         

            //Here Michal 
            //   Destroy(this.gameObject);
            housecolor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = Color.gray;
            repair = true;

          
          
                playerstats.damagePlayer();
           
              }

        if (healthregen != null)
        {
            StopCoroutine(healthregen);
        }
      
        healthregen = StartCoroutine(regainHP());


    }
    IEnumerator regainHP()
    {
      
        yield return new WaitForSeconds(3);
       
        
        while (HouseHP < maxhp)
        {
            HouseHP++;
            yield return new WaitForSeconds(.5f);
           
        }
        if(repair)
        {
            repair = false;
            GameObject.Find("FireSpawner").SendMessage("RestoreSpawner", fireposition);
            GetComponent<SpriteRenderer>().color = housecolor;
        }
    }
    

}
