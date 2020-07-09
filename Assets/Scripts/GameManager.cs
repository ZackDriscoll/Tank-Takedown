using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;
    public GameObject player;

    public GameObject enemyPrefab;
    public GameObject enemy;

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
        int playerNumber = Random.Range(0, playerSpawnPoints.Count);

        Instantiate(playerPrefab, playerSpawnPoints[playerNumber].transform.position, Quaternion.identity);
    }

    public void Respawn()
    {
        int playerNumber = Random.Range(0, playerSpawnPoints.Count);

        player.transform.position = playerSpawnPoints[playerNumber].transform.position;
    }

    public void SpawnEnemies(Room room)
    {
        enemy = Instantiate(enemyPrefab, room.enemySpawnPoint.position, Quaternion.identity);

        room.enemy = enemy;

        room.Initialize();
    }
}
