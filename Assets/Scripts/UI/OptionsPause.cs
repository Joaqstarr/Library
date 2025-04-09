using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsPause : MonoBehaviour
{
    private bool isOptionsOpen = false;

    // Variables if this is on the main menu
    public bool isMainMenu = true;
    public Button optionsButton;
    public Button tutorialButton;



    // Button that's hidden if on the main menu.
    public GameObject mainMenuButton;
    

    private void Start()
    {
        // This is here just because I am not making a whole ass script just to select a button at runtime
        // Why can I not do this in the editor.
        if (isMainMenu) { tutorialButton.Select(); }
    }

    //how do i make the game pause when pause is pressed.

    public void changeMenuState ()
    {
        if (!isOptionsOpen) {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            if (isMainMenu) { mainMenuButton.SetActive(false); }
            isOptionsOpen = true;
        }
        else {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            if (isMainMenu) { optionsButton.Select(); }
            isOptionsOpen = false;
        }
    }
    
}
