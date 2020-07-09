using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    public int rows;
    public int columns;
    public GameObject[] gridPrefabs;
    public int mapSeed;

    private float roomWidth = 50.0f;
    private float roomHeight = 50.0f;
    private Room[,] grid;

    public enum MapType
    {
        Seeded,
        Random,
        MapOfTheDay
    }

    public MapType mapType = MapType.Random;

    public int DateToInt(DateTime dateToUse)
    {
        return dateToUse.Year + dateToUse.Month + 
            dateToUse.Day + dateToUse.Hour + 
            dateToUse.Minute + dateToUse.Second + 
            dateToUse.Millisecond;
    }

    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    public void GenerateGrid()
    {
        UnityEngine.Random.InitState(mapSeed);

        //Start with an empty grid
        grid = new Room[columns, rows];

        //For each grid row
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            for (int currentColumn = 0; currentColumn < columns; currentColumn++)
            {
                //Figure out the location
                float xPosition = roomWidth * currentColumn;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0, zPosition);

                //Create a new grid at the appropriate location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                //Set the room's parent
                tempRoomObj.transform.parent = this.transform;

                //Give the room a meaningful name
                tempRoomObj.name = "Room_" + currentColumn + "," + currentRow;

                Room tempRoom = tempRoomObj.GetComponent<Room>();

                DeactivateDoors(currentRow, currentColumn, tempRoom);

                grid[currentColumn, currentRow] = tempRoom;

                GameManager.Instance.SpawnEnemies(tempRoom);
            }
        }
    }

    private void DeactivateDoors(int currentRow, int currentColumn, Room tempRoom)
    {
        if (rows == 1)
        {
            //Do nothing
        }
        else if (currentRow == 0)
        {
            tempRoom.doorNorth.SetActive(false);
        }
        else if (currentRow == rows - 1)
        {
            tempRoom.doorSouth.SetActive(false);
        }
        else
        {
            tempRoom.doorNorth.SetActive(false);
            tempRoom.doorSouth.SetActive(false);
        }

        if (columns == 1)
        {
            //Do nothing
        }
        else if (currentColumn == 0)
        {
            tempRoom.doorWest.SetActive(false);
        }
        else if (currentColumn == columns - 1)
        {
            tempRoom.doorEast.SetActive(false);
        }
        else
        {
            tempRoom.doorWest.SetActive(false);
            tempRoom.doorEast.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        switch (mapType)
        {
            case MapType.MapOfTheDay:
                mapSeed = DateToInt(DateTime.Now.Date);
                break;
            case MapType.Random:
                mapSeed = DateToInt(DateTime.Now);
                break;
            case MapType.Seeded:
                break;
            default:
                Debug.LogWarning("[MapGenerator] Map type not implemented.");
                break;
        }

        GenerateGrid();
        GameManager.Instance.SpawnPlayer();
    }
}
