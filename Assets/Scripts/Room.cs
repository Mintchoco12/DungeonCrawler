using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2Int identifier;
    public Rect bounds;
    private List<EnemySpawner> spawners;

    public Room(Vector2Int identifier, Vector2 center, Vector2 size)
    {
        //Places identifier in class variable
        this.identifier = identifier;
        
        //New list for EnemySpawners
        spawners = new List<EnemySpawner>();

        //New rect and sets size & center with help from parameters
        bounds = new Rect();
        bounds.size = size;
        bounds.center = center;
    }

    public void Activate(bool shouldActivate)
    {
        //For each spawner in list
        foreach(EnemySpawner spawner in spawners)
        {
            //Enable/Disable 
            spawner.enabled = shouldActivate;
        }
    }

    public void RegisterIfInside(EnemySpawner spawner)
    {
        //Uses contains function to see if rectangle in in the borders
        if (bounds.Contains(spawner.transform.position)) 
        {
            //If true, enemyspawner is in the room and adds it to list
            Debug.Log($"Adding {spawner.name} to {identifier}");
            spawners.Add(spawner);
        }
    }

    public bool IsPositionInside(Vector2 position)
    {
        //Return true if parameters position is in the room
        return bounds.Contains(position);
    }
}
