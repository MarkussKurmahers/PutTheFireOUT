using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RadioScript : MonoBehaviour
{
   [SerializeField] AudioClip[] Markus;
    [SerializeField] AudioClip[] Henry;
    [SerializeField] AudioClip[] Kian;
    [SerializeField] AudioClip[] Dex;
    [SerializeField] AudioClip[] Danial;
    [SerializeField] AudioClip[] Michal;

    int stationindex;
    AudioSource audioplayer;
    public Text stationname;
    public Animator radioanimator;

    private void Start()
    {
        audioplayer = GetComponent<AudioSource>();
        stationindex = PlayerPrefs.GetInt("Station");
        ChangeStation();
      
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            stationindex++;
            if(stationindex > 5)
            {
                stationindex = 0;
            }
            radioanimator.SetTrigger("pop");
            PlayerPrefs.SetInt("Station", stationindex);

            ChangeStation();

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stationindex--;
            if (stationindex < 0)
            {
                stationindex = 5;
            }
            radioanimator.SetTrigger("pop");
            PlayerPrefs.SetInt("Station", stationindex);
            ChangeStation();
        }
    }


    void ChangeStation()
    {

    
        switch(stationindex)
        {
            case 0:
                stationname.text = "Lil' Markus";
                if(Markus.Length > 0)
                {
                    audioplayer.clip = Markus[Random.Range(0, Markus.Length)];

                }

                break;
            case 1:
                stationname.text = "-HxS-";
                if(Henry.Length > 0)
                {
                    audioplayer.clip = Henry[Random.Range(0, Henry.Length)];

                }
                break;
            case 2:
                stationname.text = "K-KIAN";
                if(Kian.Length > 0 )
                {
                    audioplayer.clip = Kian[Random.Range(0, Kian.Length)];


                }
                break;
            case 3:
                stationname.text = "D3X";
                if(Dex.Length > 0)
                {
                    audioplayer.clip = Dex[Random.Range(0, Dex.Length)];


                }
                break;
            case 4:
                stationname.text = "daniel";
                if(Danial.Length > 0)
                {
                    audioplayer.clip = Danial[Random.Range(0, Danial.Length)];

                }
                break;
            case 5:
                stationname.text = "Mi chal?";
                if(Michal.Length > 0)
                {
                    audioplayer.clip = Michal[Random.Range(0, Michal.Length)];

                }
                break;

        }
        audioplayer.Play();

    }

}
