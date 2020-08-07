using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //Set variable for the high score list display size, 
    //cannot change in code appart from initializing
    private const int HIGHSCORETABLESIZE = 3;

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

    //Camera prefab to attatch to player
    public GameObject cameraPrefab;

    //GameObjects and List to keep track of the enemies spawned
    public GameObject enemyPrefab;
    public GameObject enemy;
    public List<GameObject> enemies;

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

        //Sort the high score list from highest to lowest
        scores.Sort();
        scores.Reverse();

        //Limit the size of the high score list
        //scores = scores.GetRange(index: 0, count: HIGHSCORETABLESIZE);
    }

    public void Start()
    {
        //Initialize the player at 0, 0.1f, 0 in order to have access to the camera before the game officially starts
        playerOne = Instantiate(playerPrefab, new Vector3 (0, 0.1f, 0), Quaternion.identity);
        playerOne.GetComponent<InputManager>().input = InputManager.InputScheme.WASD;
        playerOneCamera = Instantiate(cameraPrefab, playerOne.transform.position, playerOne.transform.rotation);
        playerOneCamera.GetComponent<CameraController>().player = playerOne.transform;

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

        if (twoPlayerGame == true)
        {
            playerOneCamera.GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);

            playerTwo = Instantiate(playerPrefab, rooms[playerTwoNumber].playerSpawnPoint.position, Quaternion.identity);
            playerTwo.GetComponent<InputManager>().input = InputManager.InputScheme.arrowKeys;

            GameObject playerTwoCamera = Instantiate(cameraPrefab, playerTwo.transform.position, playerTwo.transform.rotation);
            playerTwoCamera.GetComponent<CameraController>().player = playerTwo.transform;

            playerTwoCamera.GetComponent<Camera>().rect = new Rect(0, 0, 1, 0.5f);
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

        room.enemy = enemy;

        room.Initialize();
    }
}
