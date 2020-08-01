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

    public MapType mapType;

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

        Debug.Log("Columns: " + columns + "Rows: " + rows);
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

                GameManager.Instance.rooms.Add(tempRoom);

                GameManager.Instance.SpawnEnemies(tempRoom);
            }
        }
    }

    private void DeactivateDoors(int currentRow, int currentColumn, Room tempRoom)
    {
        if (currentColumn < columns - 1)
        {
            //open east door
            tempRoom.doorEast.SetActive(false);
        }

        if (currentColumn > 0)
        {
            //open west door
            tempRoom.doorWest.SetActive(false);
        }

        if (currentRow < columns - 1)
        {
            //open north door
            tempRoom.doorNorth.SetActive(false);
        }

        if (currentRow > 0)
        {
            //open south door
            tempRoom.doorSouth.SetActive(false);
        }
    }

    public void StartGame()
    {
        GenerateGrid();
        GameManager.Instance.SpawnPlayer();
    }

    public void SetSeed(int newSeed)
    {
        //Setting the new seed through options
        mapSeed = newSeed;
    }

    public void SetMapType(MapType newMapType)
    {
        //Setting the map type through options
        mapType = newMapType;
    }
}
