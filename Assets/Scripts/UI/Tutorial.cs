using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private bool isOptionsOpen = false;

    public Button optionsButton;    

    //how do i make the game pause when pause is pressed.

    public void changeMenuState ()
    {
        if (!isOptionsOpen) {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            isOptionsOpen = true;
        }
        else {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            optionsButton.Select(); 
            isOptionsOpen = false;
        }
    }
    
}
