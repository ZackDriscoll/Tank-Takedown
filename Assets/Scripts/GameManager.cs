using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : Singleton<GameManager>
{
    //Access to the map generator
    public MapGenerator mapGenerator;

    //Variable to deturmine the number of players
    public int numPlayers;

    //True = two players, False = one player
    public bool twoPlayerGame;

    //GameObjects for the player
    public GameObject playerPrefab;
    public GameObject playerOne;    
    public GameObject playerTwo;
    public GameObject playerOneCamera;
    public Canvas playerOneHUD;
    public Canvas playerTwoHUD;

    //Camera prefab to attatch to player
    public GameObject cameraPrefab;

    //GameObjects and List to keep track of the enemies spawned
    public GameObject enemyPrefab;
    public GameObject enemy;
    public List<GameObject> enemies;

    public TitleMenu titleMenu;

    public int demoNumber = 13;

    //Lists to control the number of player spawn points, rooms, and scores
    public List<PlayerSpawnPoint> playerSpawnPoints;
    public List<Room> rooms;
    public List<ScoreData> scores = new List<ScoreData>();

    //Allows the use of audio sources
    public AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        playerSpawnPoints = new List<PlayerSpawnPoint>();
    }

    public void Update()
    {
        if (scores.Count != 0)
        {
            if (SaveManager.Instance.score < scores[0].score)
            {
                SaveManager.Instance.Save(scores[0]);
            }
        }

        if (scores.Count != 0)
        {
            if (SaveManager.Instance.score < scores[1].score)
            {
                SaveManager.Instance.Save(scores[1]);
            }
        }

            
    }

    public void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        //Initialize the player at 0, 0.1f, 0 in order to have access to the camera before the game officially starts
        playerOne = Instantiate(playerPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity);
        playerOne.GetComponent<InputManager>().input = InputManager.InputScheme.WASD;
        playerOneCamera = Instantiate(cameraPrefab, playerOne.transform.position, playerOne.transform.rotation);
        playerOneCamera.GetComponent<CameraController>().player = playerOne.transform;

        if (!twoPlayerGame)
        {
            playerOneHUD.worldCamera = playerOneCamera.GetComponent<Camera>();
            playerOneHUD.planeDistance = 5;
            playerTwoHUD.enabled = false;
        }

        audioSource.clip = AudioClips.Instance.menuMusic;
        audioSource.Play();
    }

    public void SpawnPlayer()
    {
        //Spawn the player(s) at a random spawn point within the generated map

        Debug.Log("Spawning Player");

        int playerOneNumber = Random.Range(0, rooms.Count);
        int playerTwoNumber = Random.Range(0, rooms.Count);

        playerOne.transform.position = rooms[playerOneNumber].transform.position;
        playerOne.name = "Player One";

        if (twoPlayerGame == true)
        {
            playerTwoHUD.enabled = true;

            playerOneCamera.GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);
            playerOneHUD.worldCamera = playerOneCamera.GetComponent<Camera>();

            playerTwo = Instantiate(playerPrefab, rooms[playerTwoNumber].playerSpawnPoint.position, Quaternion.identity);
            playerTwo.GetComponent<InputManager>().input = InputManager.InputScheme.arrowKeys;

            playerTwo.name = "Player Two";

            GameObject playerTwoCamera = Instantiate(cameraPrefab, playerTwo.transform.position, playerTwo.transform.rotation);
            playerTwoCamera.GetComponent<CameraController>().player = playerTwo.transform;

            playerTwoCamera.GetComponent<Camera>().rect = new Rect(0, 0, 1, 0.5f);

            playerTwoHUD.worldCamera = playerTwoCamera.GetComponent<Camera>();
            playerTwoHUD.planeDistance = 5;

            ScoreData playerTwoScoreData = new ScoreData();
            scores.Add(playerTwoScoreData);
        }  

        Debug.Log("Initially spawned at: " + playerOneNumber);
        Debug.Log("Initially spawned at: " + playerTwoNumber);
    }

    public void Respawn(GameObject player)
    {
        //Respawn the player at a random spawn point if they die

        Debug.Log("First: " + player.transform.position);

        int playerNumber = Random.Range(0, rooms.Count);

        Debug.Log("playerNumber: " + rooms[playerNumber].playerSpawnPoint.position);

        player.GetComponent<InputManager>().MovePlayer(rooms[playerNumber].playerSpawnPoint.position);

        Debug.Log(rooms[playerNumber].playerSpawnPoint.position);

        Debug.Log("Second: " + player.transform.position);

        player.GetComponent<TankData>().currentHealth = player.GetComponent<TankData>().maxHealth;
    }

    public void SpawnEnemies(Room room)
    {
        //Spawn one enemy in each room and respawn them so there is always an enemy in each room

        enemy = Instantiate(enemyPrefab, room.enemySpawnPoint.position, Quaternion.identity);

        enemy.name = "Enemy";

        room.enemy = enemy;

        room.Initialize();
    }
}
