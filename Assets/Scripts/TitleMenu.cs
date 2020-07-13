using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    public void StartGame()
    {
        //What happens when the player presses the Start Game Button
        Debug.Log("[TitleMenu] Start Game");
    }

    public void OnClickOptions()
    {
        //What happens when the player presses the Options Button
        Debug.Log("[TitleMenu] Options");
    }

    public void OnClickQuit()
    {
        //What happens when the player presses the Quit Button
        Debug.Log("[TitleMenu] Quit Game");
        Application.Quit();
    }
}
