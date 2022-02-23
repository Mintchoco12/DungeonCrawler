using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyType[] enemies;
    [SerializeField] private float interval = 1;

    private float spawnTimer;
    private List<GameObject> spawnedEnemies;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spawnedEnemies = new List<GameObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        enabled = false;
    }

    private void Update()
    {
        //If current enemies is equal to max allowed enemies, stop and return
        if (enemies.Length == spawnedEnemies.Count)
        {
            return;
        }

        //Adds deltatime to spawnTimer
        spawnTimer += Time.deltaTime;

        //Spawns enemies on interval
        if (spawnTimer >= interval)
        {
            SpawnNextEnemy();
            spawnTimer = 0;
        }
    }

    private void SpawnNextEnemy()
    {
        //Saves the path for Resources.Load
        string enemyPathName = "";

        //Takes the right path
        switch (enemies[spawnedEnemies.Count])
        {
            case EnemyType.RedOctorok:
                enemyPathName = "Prefabs/Enemies/RedOctorok";
                break;
        }

        //Get the gameobject from resources folder and instantiate on location of gameobject
        GameObject go = Instantiate(Resources.Load<GameObject>(enemyPathName), transform.position, Quaternion.identity);
        //Adds enemie to list
        spawnedEnemies.Add(go);
    }

    private void OnDisable()
    {
        //Destroys all gameobjects in spawnedEnemies
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            Destroy(spawnedEnemies[i]);
        }

        spawnedEnemies.Clear();
        spawnTimer = 0;
        spriteRenderer.material.color = Color.red;
    }

    private void OnEnable()
    {
        spriteRenderer.material.color = Color.white;
    }
}

