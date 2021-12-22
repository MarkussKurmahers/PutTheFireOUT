using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class HoseFollow : MonoBehaviour
{
    public Camera camera;
    [SerializeField] float offset;
    public GameObject sprayAnim;
    Animator animator;


    private void Start()
    {
        animator = sprayAnim.GetComponentInParent<Animator>();
    }

    public void SprayOn()
    {
        sprayAnim.SetActive(true);
    }
    public void SprayOff()
    {
        sprayAnim.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //blackthorn inspired
        Vector3 difference = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if(Input.GetMouseButtonDown(0))
        {
        
            animator.SetBool("on", true);
        }
        if(Input.GetMouseButtonUp(0))
        {
        
            animator.SetBool("on", false);


        }

    }
}
