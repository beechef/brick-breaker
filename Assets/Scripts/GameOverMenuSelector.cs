﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuSelector : VerticalMenuSelector
{
    private readonly string MENU_OPTION_GO_AGAIN = "MenuOptionGoAgain";

    /**
     * Before first frame update. 
     */
    void Start()
    {
        transform.position = GetMenuSelectorPosition();
    }

    /**
     * Update per frame.
     */
    void Update()
    {
        // invokes base class up/down arrows handling
        HandleUpDownArrowPresses();
        
        // enter case handling
        if (Input.GetKeyDown(KeyCode.Return)) HandleReturn();
    }
    
    /**
     * Handles ENTER pressing that allows the user to choose an option.
     */
    private void HandleReturn()
    {
        GameObject currentMenu = GetCurrentMenu();

        if (currentMenu.name == MENU_OPTION_GO_AGAIN)
            SceneManager.LoadScene("Level");
        else
            Application.Quit();
    }
}
