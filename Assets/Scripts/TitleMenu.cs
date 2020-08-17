using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    //GameObjects to manipulate the UI
    public GameObject titleScreen;
    public GameObject optionsMenu;
    public GameObject gameOver;

    public Text highScoreText;
    public Text playerOneScoreText;
    public Text playerTwoScoreText;

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
        audioSource.clip = AudioClips.Instance.buttonClick;
        audioSource.Play();

        //What happens when the player presses the Start Game Button
        Debug.Log("[TitleMenu] Start Game");

        titleScreen.SetActive(false);
        optionsMenu.SetActive(false);
        gameOver.SetActive(false);

        ScoreData newScoreData = new ScoreData();
        GameManager.Instance.scores.Add(newScoreData);

        //When the Start Button is pressed, play the game
        Time.timeScale = 1;

        //Don't generate the map until after Start is pressed
        GameManager.Instance.mapGenerator.StartGame();

        //Set music to the game background music
        GameManager.Instance.audioSource.clip = AudioClips.Instance.gameMusic;
        GameManager.Instance.audioSource.Play();
    }

    public void ResetGame()
    {
        foreach (GameObject objectsToDestroy in GameObject.FindGameObjectsWithTag("Room"))
        {
            if (objectsToDestroy.name == "Room")
            {
                Destroy(objectsToDestroy);
            }
        }

        foreach (GameObject objectsToDestroy in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (objectsToDestroy.name == "Player")
            {
                Destroy(objectsToDestroy);
            }
        }

        foreach (GameObject objectsToDestroy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (objectsToDestroy.name == "Enemy")
            {
                Destroy(objectsToDestroy);
            }
        }

        foreach (GameObject objectsToDestroy in GameObject.FindGameObjectsWithTag("Powerup"))
        {
            Destroy(objectsToDestroy);
        }

        foreach (GameObject objectsToDestroy in GameObject.FindGameObjectsWithTag("MainCamera"))
        {
            Destroy(objectsToDestroy);
        }

        GameManager.Instance.StartGame();
        StartGame();
    }

    //Go to the Options Menu
    public void OnClickOptions()
    {
        audioSource.clip = AudioClips.Instance.buttonClick;
        audioSource.Play();

        //What happens when the player presses the Options Button
        Debug.Log("[TitleMenu] Options");

        titleScreen.SetActive(false);
        optionsMenu.SetActive(true);
        gameOver.SetActive(false);
    }

    //Quit the game from the Title Screen/Game Over Screen
    public void OnClickQuit()
    {
        audioSource.clip = AudioClips.Instance.buttonClick;
        audioSource.Play();

        //What happens when the player presses the Quit Button
        Debug.Log("[TitleMenu] Quit Game");
        Application.Quit();
    }

    //Go back to the Title Screen when you click "Return"
    public void OnClickReturn()
    {
        audioSource.clip = AudioClips.Instance.buttonClick;
        audioSource.Play();

        titleScreen.SetActive(true);
        optionsMenu.SetActive(false);
        gameOver.SetActive(false);
    }

    //Activate the Game Over Screen when the player(s) run out of lives
    public void GameOver()
    {
        titleScreen.SetActive(false);
        optionsMenu.SetActive(false);
        gameOver.SetActive(true);
        playerTwoScoreText.enabled = false;

        Time.timeScale = 0;

        highScoreText.text = "High Score: " + SaveManager.Instance.score.ToString();

        if (GameManager.Instance.scores.Count != 0)
        {
            playerOneScoreText.text = "Player One: " + GameManager.Instance.scores[0].score.ToString();
        }

        if (GameManager.Instance.twoPlayerGame)
        {
            playerTwoScoreText.enabled = true;

            if (GameManager.Instance.scores.Count != 0)
            {
                playerTwoScoreText.text = "Player Two: " + GameManager.Instance.scores[1].score.ToString();
            }
        }

    }
}
