using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuscript : MonoBehaviour
{
    public GameObject[] sounds;
    public void soundPlay(int index)
    {
        Instantiate(sounds[index], transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) )
        {
            SceneManager.LoadScene("SampleScene");
        }
    }


}
