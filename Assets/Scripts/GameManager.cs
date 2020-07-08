using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;
    public int demoNumber = 13;
    public List<PlayerSpawnPoint> playerSpawnPoints;

    protected override void Awake()
    {
        base.Awake();
        playerSpawnPoints = new List<PlayerSpawnPoint>();
    }

    public void SpawnPlayer()
    {
        //TODO: Write code to spawn the player at a random spawn point
    }
}
