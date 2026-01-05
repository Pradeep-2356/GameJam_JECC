using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Terrain terrain;
    public GameObject enemyPrefab;
    public int spawnCount = 10;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        TerrainData data = terrain.terrainData;
        Vector3 terrainPos = terrain.transform.position;

        for (int i = 0; i < spawnCount; i++)
        {
            float x = Random.Range(0f, data.size.x);
            float z = Random.Range(0f, data.size.z);

            float y = terrain.SampleHeight(
                new Vector3(x + terrainPos.x, 0, z + terrainPos.z)
            ) + terrainPos.y;

            Vector3 spawnPos = new Vector3(
                x + terrainPos.x,
                y,
                z + terrainPos.z
            );

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }
}
