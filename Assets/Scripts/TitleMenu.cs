using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    //GameObjects to manipulate the UI
    public GameObject titleScreen;
    public GameObject optionsMenu;
    public GameObject gameOver;
    public GameObject HUD;

    //Allows the use of audio in the game
    public AudioSource audioSource;

    //Reference to the TankData Script for access to player(s) lives
    private TankData data;

    public void Start()
    {
        //On Start pause the game
        Time.timeScale = 0;

        data = gameObject.GetComponent<TankData>();
    }

    //Start the game
    public void StartGame()
    {
        /*audioSource.clip = AudioClips.audioClips.buttonClick;
        audioSource.Play();*/

        //What happens when the player presses the Start Game Button
        Debug.Log("[TitleMenu] Start Game");

        titleScreen.SetActive(false);
        optionsMenu.SetActive(false);
        gameOver.SetActive(false);
        HUD.SetActive(true);

        //When the Start Button is pressed, play the game
        Time.timeScale = 1;

        //Don't generate the map until after Start is pressed
        GameManager.Instance.mapGenerator.StartGame();

        //Set music to the game background music
        /*GameManager.Instance.audioSource.clip = AudioClips.audioClips.gameMusic;
        GameManager.Instance.audioSource.Play();*/
    }

    //Go to the Options Menu
    public void OnClickOptions()
    {
        /*audioSource.clip = AudioClips.audioClips.buttonClick;
        audioSource.Play();*/

        //What happens when the player presses the Options Button
        Debug.Log("[TitleMenu] Options");

        titleScreen.SetActive(false);
        optionsMenu.SetActive(true);
        gameOver.SetActive(false);
        HUD.SetActive(false);
    }

    //Quit the game from the Title Screen/Game Over Screen
    public void OnClickQuit()
    {
        /*audioSource.clip = AudioClips.audioClips.buttonClick;
        audioSource.Play();*/

        //What happens when the player presses the Quit Button
        Debug.Log("[TitleMenu] Quit Game");
        Application.Quit();
    }

    //Go back to the Title Screen when you click "Return"
    public void OnClickReturn()
    {
        /*audioSource.clip = AudioClips.audioClips.buttonClick;
        audioSource.Play();*/

        titleScreen.SetActive(true);
        optionsMenu.SetActive(false);
        gameOver.SetActive(false);
        HUD.SetActive(false);
    }

    //Activate the Game Over Screen when the player(s) run out of lives
    public void GameOver()
    {
        if (data.lives <= 0)
        {
            titleScreen.SetActive(false);
            optionsMenu.SetActive(false);
            gameOver.SetActive(true);
            HUD.SetActive(false);
        }
    }
}
