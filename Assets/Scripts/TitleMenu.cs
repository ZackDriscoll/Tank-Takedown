using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject optionsMenu;
    public GameObject gameOver;

    public void Start()
    {
        //On Start pause the game
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        //What happens when the player presses the Start Game Button
        Debug.Log("[TitleMenu] Start Game");

        titleScreen.SetActive(false);
        optionsMenu.SetActive(false);
        //gameOver.SetActive(false);

        //When the Start Button is pressed, play the game
        Time.timeScale = 1;
    }

    public void OnClickOptions()
    {
        //What happens when the player presses the Options Button
        Debug.Log("[TitleMenu] Options");

        titleScreen.SetActive(false);
        optionsMenu.SetActive(true);
        //gameOver.SetActive(false);
    }

    public void OnClickQuit()
    {
        //What happens when the player presses the Quit Button
        Debug.Log("[TitleMenu] Quit Game");
        Application.Quit();
    }

    public void OnClickReturn()
    {
        titleScreen.SetActive(true);
        optionsMenu.SetActive(false);
        //gameOver.SetActive(false);
    }
}
