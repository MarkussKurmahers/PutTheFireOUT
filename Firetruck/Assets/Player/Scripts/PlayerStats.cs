using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
   public int score;
   // int bestScore;
    int maxHealth = 5;
    int currentHealth;
    public TextMeshProUGUI textField;
    [SerializeField] HealthVisual health;
    public GameObject healsound;
    public GameObject damagesound;
    
    private void Start()
    {
        currentHealth = maxHealth;
        score = 0;

    }

    private void FixedUpdate()
    {
        displayScore(score);  // changes text mesh pro field to display score
        PlayerPrefs.SetFloat("score", score);
        
    }



    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    // reduce player's health by 1 heart
    public void damagePlayer()
    {
        currentHealth -= 1;
        Instantiate(damagesound, transform.position, Quaternion.identity);
        health.updateHealth();
    }

    // increases player's health by 1 heart
    public void addHealth()
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += 1;
            Instantiate(healsound, transform.position, Quaternion.identity); 

        }


        health.updateHealth();
    }


    public void addPoints(int pointsToAdd)
    {
        score += pointsToAdd;
    }


    void displayScore(int points)
    {
        points = score;
        if (points < 10)
        {
            textField.text = "000" + points.ToString();
        }
        else if (points < 100)
        {
            textField.text = "00" + points.ToString();
        }
        else if (points < 1000)
        {
            textField.text = "0" + points.ToString();
        }
        else if (points >= 1000)
        {
            textField.text = points.ToString();
        }
    }





}
