using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float SmoothSpeed,rotationspeed;
    public Vector3 rotationoffset;
    public Vector3 offset;


    [SerializeField] bool speedinfluenced;
    Camera cam;
    float ogcamerasize;
    PlayerControll body;

  
    private void Start()
    {
        if(speedinfluenced)
        {
             body = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();
            cam = GetComponent<Camera>();
            ogcamerasize = cam.orthographicSize;

        }
        
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + target.right * offset.x + target.up * offset.y + target.forward * offset.z;
        Vector3 smoothposition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
        transform.position = smoothposition;



        Quaternion desiredrotation = target.rotation;
        desiredrotation *= Quaternion.Euler(rotationoffset);

        Quaternion smoothrotation = Quaternion.Lerp(transform.rotation, desiredrotation, rotationspeed);

        transform.rotation = smoothrotation;



     if(speedinfluenced)
        {
          
            if(body.speed > body.maxspeed / 2)
            {

                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, ogcamerasize * 1.2f, 1 * Time.deltaTime );
                offset.x = Mathf.Lerp(offset.x, 2, 1 * Time.deltaTime);

            }
            else
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, ogcamerasize, 2 * Time.deltaTime);
                offset.x = Mathf.Lerp(offset.x, 0, 2 * Time.deltaTime);

            }
        }
     
        
    }

}
