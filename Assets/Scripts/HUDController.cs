using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    //UI Text to manipulate
    public Text scoreText;
    public Text livesText;

    //Variables used from TankData and ScoreData
    private TankData data;
    public ScoreData scoreData;
    public float Score;
    public int Lives;

    void Awake()
    {
        //Don't destroy UI when loading next scene
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        data = gameObject.GetComponent<TankData>();
        /*scoreData = gameObject.GetComponent<ScoreData>();*/
    }

    void Update()
    {
        /*//Everytime I call update, update the scores and lives from TankData and SoreData
        Score = scoreData.score;
        Lives = data.lives;

        //Change text based on player score and lives values
        scoreText.text = "X " + Score.ToString();
        livesText.text = "X " + Lives.ToString();*/
    }
}
