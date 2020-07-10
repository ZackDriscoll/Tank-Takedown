using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour 
{

	public GameObject doorNorth;
	public GameObject doorSouth;
	public GameObject doorEast;
	public GameObject doorWest;

	public Transform[] waypoints;

	public Transform enemySpawnPoint;
	public GameObject enemy;

	public Transform playerSpawnPoint;

	private AIController ac;

	public void Initialize()
	{
		ac = enemy.GetComponent<AIController>();

		ac.waypoints = waypoints;

		int randomNumber = Random.Range(0, (int)AIController.AIPersonality.Coward);
		ac.currentPersonality = (AIController.AIPersonality)randomNumber;
	}

	void Update()
	{
		if (enemy == null)
		{
			GameManager.Instance.SpawnEnemies(this);
		}
	}

}
