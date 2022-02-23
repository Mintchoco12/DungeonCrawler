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
        if (enemies.Length == spawnedEnemies.Count)
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= interval)
        {
            SpawnNextEnemy();
            spawnTimer = 0;
        }
    }

    private void SpawnNextEnemy()
    {
        string enemyPathName = "";

        switch (enemies[spawnedEnemies.Count])
        {
            case EnemyType.RedOctorok:
                enemyPathName = "Prefabs/Enemies/RedOctorok";
                break;
        }
        GameObject go = Instantiate(Resources.Load<GameObject>(enemyPathName), transform.position, Quaternion.identity);

        spawnedEnemies.Add(go);
    }

    private void OnDisable()
    {
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

