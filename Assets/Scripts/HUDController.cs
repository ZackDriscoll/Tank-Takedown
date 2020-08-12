using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    //UI Text to manipulate
    public Text playerOneScoreText;
    public Text playerOneLivesText;

    public Text playerTwoScoreText;
    public Text playerTwoLivesText;

    //Variables used from TankData and ScoreData
    public int Score;
    public int Lives;

    void Awake()
    {
        //Don't destroy UI when loading next scene
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        //Change text based on player score and lives values
        if (GameManager.Instance.scores.Count != 0)
        {
            playerOneScoreText.text = "X " + GameManager.Instance.scores[0].score.ToString(); 
        }
        playerOneLivesText.text = "X " + GameManager.Instance.playerOne.GetComponent<TankData>().lives.ToString();

        //Everytime I call update, update the scores and lives from TankData and SoreData
        if (GameManager.Instance.twoPlayerGame == true)
        {
            //Change text based on player score and lives values
            if (GameManager.Instance.scores.Count != 0)
            {
                playerTwoScoreText.text = "X " + GameManager.Instance.scores[1].score.ToString(); 
            }
            playerTwoLivesText.text = "X " + GameManager.Instance.playerTwo.GetComponent<TankData>().lives.ToString();
        }

        
    }
}
