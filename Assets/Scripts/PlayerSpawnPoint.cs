using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.playerSpawnPoints.Add(this.gameObject.GetComponent<PlayerSpawnPoint>());
    }

    void OnDestroy()
    {
        GameManager.Instance.playerSpawnPoints.Remove(this.gameObject.GetComponent<PlayerSpawnPoint>());
    }
}
