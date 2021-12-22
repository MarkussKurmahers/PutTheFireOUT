using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    public GameObject debriparticle;
    public GameObject soundprefab;
    public float scale = 1;

    public float speed2break;
    float ogspeed;
    PlayerControll controller;
    private void OnCollisionEnter2D(Collision2D collision)
    {
     if(collision.gameObject.CompareTag("Player"))
        {
             controller = collision.gameObject.GetComponent<PlayerControll>();
            if (controller.speed > speed2break && Input.GetAxisRaw("Vertical") > 0 )  
            {
              
                ogspeed = controller.speed;
                if(debriparticle)
                {
                    var spriteimg = GetComponent<SpriteRenderer>().sprite;

                   GameObject particleobj = Instantiate(debriparticle, collision.contacts[0].point, collision.transform.rotation);
                    ParticleSystem particlecomp = particleobj.GetComponent<ParticleSystem>();
                    var spritesheet = particlecomp.textureSheetAnimation;
                    spritesheet.SetSprite(0, spriteimg);
                    particleobj.transform.localScale = new Vector3(scale, scale, 0);
                }
                if (soundprefab)
                {
                    Instantiate(soundprefab, transform.position, Quaternion.identity);

                }
                
                Destroy(gameObject);
            }
        }

     
    }


    private void OnDestroy()
    {
        if(controller)
        {
            controller.setSpeed(ogspeed);

        }
    }

}
