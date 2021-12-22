using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float accelaration;
    public float turnrate;
    public float speed;
    public float maxspeed;
    public float drift;

    float rotationangle;

   


   public bool breaking;
   

   public Rigidbody2D body;

    float speedInput;
    float turnInput;
    public GameObject obstacles;
     BoxCollider2D[] ignorecollision;
    BoxCollider2D thiscollider;

    float boost;

    public GameObject boostSFX;


    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.gameObject.GetComponentInChildren<PlayerParticles>().ParentCollision(collision);
    }



    private void Start()
    {

        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        ignorecollision = obstacles.GetComponents<BoxCollider2D>();
        thiscollider = GetComponent<BoxCollider2D>();
        rotationangle = transform.rotation.z;
        body = GetComponent<Rigidbody2D>();
        body.AddForce(transform.right*5,ForceMode2D.Impulse);
        for(int i=0;i<ignorecollision.Length;i++)
        {
            Physics2D.IgnoreCollision(ignorecollision[i], thiscollider, true);
        }
    }

    public void setSpeed(float value)
    {
        Vector2 force = transform.right * value;
        body.velocity = force;
      
    }
   

    void Update()
    {
        speedInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            drift *= 3;
            turnrate *= 1.3f;
            breaking = true;
           

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            drift /= 3;
            turnrate /= 1.3f;

            breaking = false;


        }
       
      

    }


    private void FixedUpdate()
    {
       
        engineforce();
        reducedrift();
        stearing();
    }

    void reducedrift()
    {
     


        Vector2 fowardvelocity = transform.right * Vector2.Dot(body.velocity, transform.right);
        Vector2 sidevelocity = transform.up * Vector2.Dot(body.velocity, transform.up);

        body.velocity = fowardvelocity + sidevelocity * drift;
    }

    void engineforce()
    {
        speed = Vector2.Dot(transform.right, body.velocity);

        if(speed > maxspeed && speedInput > 0)
        {
            return;
        }
        if(speed < -maxspeed * .5f && speedInput < 0)
        {
            return;
        }
        if(body.velocity.sqrMagnitude > maxspeed * maxspeed && speedInput > 0)
        {
            return;
        }    
        if(breaking)
        {
            if(speedInput >0)
            {
                body.drag = Mathf.Lerp(body.drag, .3f, Time.fixedDeltaTime);
                if(speed > 3)
                {
                    boost += Time.deltaTime;

                }

            }
            else
            {
                body.drag = Mathf.Lerp(body.drag, 4, Time.fixedDeltaTime);
            }

         
            return;
        }    


        if(speedInput == 0)
        {
            body.drag = Mathf.Lerp(body.drag, 3, Time.fixedDeltaTime);
        }
        else
        {
            body.drag = 0;
        }

      
        Vector2 force = transform.right * speedInput * accelaration;
        if (boost > 0 )
        {
            Instantiate(boostSFX, transform.position, Quaternion.identity);
            force *= boost * 100;
            boost = 0;
        }
        body.AddForce(force, ForceMode2D.Force);

    }
    void stearing()
    {
        float minspeed = body.velocity.magnitude / 7;
        minspeed = Mathf.Clamp01(minspeed);

        int i = 1;
        if(speed < 0)
        {
            i = -1;
        }

        rotationangle -= turnInput * turnrate * minspeed * i;
        body.MoveRotation(rotationangle);
    }

}
