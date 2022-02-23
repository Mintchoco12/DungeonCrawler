using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Game : MonoBehaviour
{
    [SerializeField] private int roomsAmountHorizontal;
    [SerializeField] private int roomsAmountVertical;
    [SerializeField] private Vector2 roomSize;
    [SerializeField] private Vector2 roomOffset;

    private PlayerMovement playerMovement;
    private Room[,] rooms;
    private Room currentRoom;

    private void Start()
    {
        //Save playermovement
        playerMovement = FindObjectOfType<PlayerMovement>();

        //Creates the rooms and them in the right position
        BuildRooms();

        //Register EnemySpawners
        RegisterSpawners();

        //Activate starting room
        currentRoom = GetRoomWithWorldPosition(playerMovement.transform.position);
        //If room is found
        if (currentRoom != null) 
        {
            //Activate room
            currentRoom.Activate(true);
        }
    }
     
    private void BuildRooms()
    {
        //Creates 2D array with correct size
        rooms = new Room[roomsAmountVertical, roomsAmountHorizontal];

        //For every row and column in the grid
        for (int row = 0; row < rooms.GetLength(0); row++)
        {
            for (int column = 0; column < rooms.GetLength(1); column++)
            {
                //Create a identifier with x: column, y: row
                Vector2Int identifier = new Vector2Int(column, row);

                //Calculate the middle of room
                Vector2 centerOfRoom = new Vector2(column * roomSize.x, row * roomSize.y);
                //Adds offset to room
                centerOfRoom += roomOffset;

                //Create room, with help from constructor gets all data
                rooms[row, column] = new Room(identifier, centerOfRoom, roomSize);
            }
        }
    }

    private void RegisterSpawner(EnemySpawner spawner)
    {
        //foreach room in rooms
        foreach(Room room in rooms)
        {
            //Register spawner if its inside room
            room.RegisterIfInside(spawner);
        }
    }

    private void RegisterSpawners()
    {
        //Find all enemy spawners
        EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();

        //Register all enemy spawners with RegisterSpawner
        foreach(EnemySpawner spawner in spawners)
        {
            RegisterSpawner(spawner);
        }
    }

    private Room GetRoomWithWorldPosition(Vector2 position)
    {
        Room room = null;

        foreach(Room r in rooms)
        {
            //If parameters position is in room
            if (r.IsPositionInside(position))
            {
                //room variable = r variable
                room = r;
                break;
            }
        }

        //Return the room that was found
        return room;
    }

    public void ActivateRoom(Vector2 position)
    {
        Room room = GetRoomWithWorldPosition((Vector2)position);

        if (room != null)
        {
            currentRoom = room;
            currentRoom.Activate(true);
        }
    }

    public void DeactivateCurrentRoom()
    {
        if (currentRoom != null)
        {
            currentRoom.Activate(false);
        }
    }

    private void OnDrawGizmos()
    {
        if (rooms == null)
        {
            return;
        }

        foreach(Room room in rooms)
        {
            Gizmos.DrawWireCube(room.bounds.center, room.bounds.size);
            if (Application.isEditor)
            {
                Handles.Label(room.bounds.center, room.identifier.ToString());
            }
        }
    }
}
