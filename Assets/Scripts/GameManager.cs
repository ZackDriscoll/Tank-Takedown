using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;
    public GameObject player;

    public GameObject cameraPrefab;

    public GameObject enemyPrefab;
    public GameObject enemy;
    public List<GameObject> enemies;

    public int demoNumber = 13;
    public List<PlayerSpawnPoint> playerSpawnPoints;
    public List<Room> rooms;

    protected override void Awake()
    {
        base.Awake();
        playerSpawnPoints = new List<PlayerSpawnPoint>();
    }

    public void SpawnPlayer()
    {
        Debug.Log("Spawning Player");

        int playerNumber = Random.Range(0, rooms.Count);

        Instantiate(playerPrefab, rooms[playerNumber].playerSpawnPoint.position, Quaternion.identity);

        Instantiate(cameraPrefab, rooms[playerNumber].playerSpawnPoint.position, Quaternion.identity);

        player = FindObjectOfType<InputManager>().gameObject;

        Debug.Log("Initially spawned at: " + playerNumber);
    }

    public void Respawn()
    {
        Debug.Log("First: " + player.transform.position);

        int playerNumber = Random.Range(0, rooms.Count);

        player.GetComponent<InputManager>().MovePlayer(rooms[playerNumber].playerSpawnPoint.position);

        Debug.Log(rooms[playerNumber].playerSpawnPoint.position);

        Debug.Log("Second: " + player.transform.position);

        player.GetComponent<TankData>().currentHealth = player.GetComponent<TankData>().maxHealth;
    }

    public void SpawnEnemies(Room room)
    {
        enemy = Instantiate(enemyPrefab, room.enemySpawnPoint.position, Quaternion.identity);

        room.enemy = enemy;

        room.Initialize();
    }
}
