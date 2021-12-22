using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    public PlayerControll controller;
    public TrailRenderer[] tiretrail;
    public ParticleSystem driftsmokeparticle;
    public AudioSource driftsound;

    public AudioSource drivesound;
    public AudioSource bumpsound;
    public GameObject sparkleparticle;

    public void ParentCollision(Collision2D other)
    {
        if (Mathf.Abs (controller.speed ) > 1.5f)
        {
            bumpsound.pitch = Random.Range(.9f, 1.1f);
            bumpsound.volume = Mathf.Abs( controller.speed / 3);
            Instantiate(sparkleparticle, other.contacts[0].point, Quaternion.identity);
            bumpsound.Play();
        }
    }

    

    private void Update()
    {
        if(!drivesound.isPlaying && controller.body.velocity.magnitude > 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            drivesound.Play();
        }
        if(drivesound.isPlaying)
        {
            drivesound.volume = Mathf.Lerp(drivesound.volume, Mathf.Clamp(controller.speed, 2, controller.maxspeed), .1f *Time.deltaTime );
            drivesound.pitch = Mathf.Lerp(drivesound.volume, Mathf.Clamp(controller.speed, 2, controller.maxspeed), .1f * Time.deltaTime);
        }
        if(Input.GetAxisRaw("Vertical") == 0)
        {
            drivesound.volume = Mathf.Lerp(drivesound.volume, -Mathf.Clamp(controller.speed, 2, controller.maxspeed), .3f * Time.deltaTime);
            drivesound.pitch = Mathf.Lerp(drivesound.pitch, -Mathf.Clamp(controller.speed, 2, controller.maxspeed), .3f * Time.deltaTime);


        }


        if (controller.breaking && controller.body.velocity.magnitude > 3)
        {
            if(!driftsound.isPlaying)
            {
                driftsound.Play();
              

            }
            if(driftsound.isPlaying)
            {
                driftsound.pitch = Mathf.Lerp(driftsound.pitch, 1.4f, 15 * Time.deltaTime);

            }
           for(int i=0; i < tiretrail.Length;i++)
            {
                tiretrail[i].emitting = true;

            }
           if(driftsmokeparticle)
            {
                var emission = driftsmokeparticle.emission;
                emission.rateOverDistance = 20;
                          

            }


        }
        else
        {
            for (int i = 0; i < tiretrail.Length; i++)
            {
                tiretrail[i].emitting = false;
            }

            if (driftsmokeparticle)
            {
                var emission = driftsmokeparticle.emission;
                emission.rateOverDistance = 0;

            }

            if (driftsound)
            {
                driftsound.pitch = 1;
                driftsound.Stop();


            }
        }



    }
}
