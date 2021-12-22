using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class BasicCarAI : MonoBehaviour
{

     AIDestinationSetter destination;
    public Transform[] waypoints;
     AIPath Carsettings;
    public bool stop;
    Rigidbody2D body;
    
    public float speed;
    bool crash;
    float timeout=6;
    [SerializeField] float Mindestroydistance;

    public GameObject explosioneparticles;
    public GameObject explosionSound;
    public GameObject smoke;
    public GameObject[] HornSounds;
   [SerializeField] int maxhealth;
    int health;

    private void OnDestroy()
    {
        if (GameObject.Find("CarSpawner"))
        {
            GameObject.Find("CarSpawner").SendMessage("LessCap");

        }
    }
    public void remap()
    {

       stop = true;
       
       

        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
       
       
        yield return new WaitForSeconds(Random.Range(1,2));
        
        stop = false;
    }
   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!crash)
        {
            body.AddForce(transform.forward * -1, ForceMode2D.Force);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
          
            crash = true;
            Carsettings.canMove = false;
            body.AddForce(collision.gameObject.transform.right * collision.contacts[0].collider.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / body.mass,ForceMode2D.Impulse);
            StopCoroutine(crashwait());
            StartCoroutine(crashwait());

            health--;

            if (health>0 && health < 70)
            {
                Instantiate(HornSounds[Random.Range(0, HornSounds.Length)],transform.position,Quaternion.identity );
            }
            if(health <= maxhealth/2)
            {
                smoke.SetActive(true);
            }
            if(health<=0)
            {
                int a = Random.Range(0, 2);
                if(a == 1)
                {
                 GameObject temp =   Instantiate(HornSounds[Random.Range(0, HornSounds.Length)], transform.position, Quaternion.identity);
                    temp.GetComponent<AudioSource>().pitch = .5f;
                }

                health = 99;
                Instantiate(explosioneparticles, transform.position, Quaternion.identity);
                Instantiate(explosionSound, transform.position, Quaternion.identity);
                stop = true;
                GetComponentInChildren<SpriteRenderer>().color = Color.black;
                StartCoroutine(fadeaway());
            }


        }
       
    }

    IEnumerator fadeaway()
    {
        yield return new WaitForSeconds(4);
        var black = GetComponentInChildren<SpriteRenderer>().color;
        while (GetComponentInChildren<SpriteRenderer>().color.a > 0)
        {
            yield return null;
            GetComponentInChildren<SpriteRenderer>().color = new Color(black.r, black.g, black.b, GetComponentInChildren<SpriteRenderer>().color.a - .01f);
        }
        Destroy(gameObject);
    }


  IEnumerator crashwait()
    {
        yield return new WaitForSeconds(3);
      
        crash = false;
            Carsettings.canMove = true;
        
    }

    void Start()
    {
        waypoints = GameObject.Find("AIWaypoints").GetComponentsInChildren<Transform>();

        health = maxhealth;
        destination = GetComponent<AIDestinationSetter>();
        Carsettings = GetComponent<AIPath>();
        body = GetComponent<Rigidbody2D>();

        CalculateTragetory();
    }

     void CalculateTragetory()
    {
        int index = 0;
        float topdistance = Vector2.Distance(transform.position, waypoints[0].position);
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            float currentdistance = Vector2.Distance(waypoints[i + 1].position, transform.position);
            if (topdistance < currentdistance)
            {
                topdistance = currentdistance;
                index = i + 1;

            }
        }

        destination.target = waypoints[index];
    }


    // Update is called once per frame
    void Update()
    {
        if(stop)
        {
            Carsettings.canMove = false;
        }
        else
        {
            if(!crash)
            {
                Carsettings.canMove = true;
            }
           

        }
        if (stop)
        {
            body.AddTorque(.1f);
            body.AddForce(transform.up * -1 * speed,ForceMode2D.Force);
        }
        
        if(timeout < 0)
        {
            timeout = 6;
            remap();
        }
       
        if(Carsettings.velocity.magnitude < 0.1f)
        {
            timeout -= Time.deltaTime;
        }

        if(Carsettings.reachedDestination)
        {
            if ( Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) > Mindestroydistance)
            {
                Destroy(gameObject);
            }
          else
            {
                CalculateTragetory();
            }

        }
       
      

    }
}
