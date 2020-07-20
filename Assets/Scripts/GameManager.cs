using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //Set variable for the high score list display size, 
    //cannot change in code appart from initializing
    private const int HIGHSCORETABLESIZE = 3;

    //GameObjects for the player
    public GameObject playerPrefab;
    public GameObject player;

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

    protected override void Awake()
    {
        base.Awake();
        playerSpawnPoints = new List<PlayerSpawnPoint>();

        //Sort the high score list from highest to lowest
        scores.Sort();
        scores.Reverse();

        //Limit the size of the high score list
        scores = scores.GetRange(index: 0, count: HIGHSCORETABLESIZE);
    }

    public void SpawnPlayer()
    {
        //Spawn the player at a random spawn point within the generated map

        Debug.Log("Spawning Player");

        int playerNumber = Random.Range(0, rooms.Count);

        Instantiate(playerPrefab, rooms[playerNumber].playerSpawnPoint.position, Quaternion.identity);

        Instantiate(cameraPrefab, rooms[playerNumber].playerSpawnPoint.position, Quaternion.identity);

        player = FindObjectOfType<InputManager>().gameObject;

        Debug.Log("Initially spawned at: " + playerNumber);
    }

    public void Respawn()
    {
        //Respawn the player at a random spawn point if they die

        Debug.Log("First: " + player.transform.position);

        int playerNumber = Random.Range(0, rooms.Count);

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
