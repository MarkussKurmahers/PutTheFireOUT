using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoseSwap2 : MonoBehaviour
{
    [SerializeField] private GameObject[] HoseTypes = null;
    [SerializeField] private KeyCode[] HosePos = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };
    [SerializeField] private int CurrentPosition = 0;
   
    // Start is called before the first frame update
    //Link to weapon swap script on unity: https://answers.unity.com/questions/1775103/2d-weapon-switching-1.html
    //
  
    void Start()//The start function makes sure that any Hose type above position 0 is always changed 
    {
        if (HoseTypes.Length > 0)
        {
            DisableHoses();
        }       
        CurrentPosition = 0;
        selectedHose(CurrentPosition);
    }

 /*This update function handles the information for when the player presses 
  * either 1, 2 or 3. When a player presses this button, the selectedHose method is called
  * which allows the player will define what position they're in the array. */
    void Update()
    {
        for (int a = 0; a < HosePos.Length; ++a)
        {
            if (Input.GetKeyDown(HosePos[a]))
            {
                selectedHose(a);
            }
        }
    }
   
    private void selectedHose(int i)
    {
        HoseTypes[CurrentPosition].SetActive(false);
        CurrentPosition = i;
        HoseTypes[CurrentPosition].SetActive(true);
    }
/* The DisableHoses method will set the other hoses that aren't selected to false.
 * this will prevent the chance of 2 water types from firing. */
    private void DisableHoses()
    {
        for (int x = 0; x < HoseTypes.Length; ++x)
        {
            HoseTypes[x].SetActive(false);
        }
        

    }


}
