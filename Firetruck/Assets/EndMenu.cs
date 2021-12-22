using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenu : MonoBehaviour
{



  
    public TextMeshProUGUI Highscore;
    public TextMeshProUGUI Score;
    public float highscore;
    public float score;

    //playerStats.score;


    void Start()
    {

        score = PlayerPrefs.GetFloat("score");
        
                             
        Score.SetText(score.ToString());
       

        if (score > highscore)
        {          
            
            PlayerPrefs.SetFloat("highscore", score);
      

        }
        highscore = PlayerPrefs.GetFloat("highscore");
        Highscore.SetText(highscore.ToString());
    }





    public void Restart()
    {
       
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
       
        SceneManager.LoadScene("MainMenu");
    }






    
}
