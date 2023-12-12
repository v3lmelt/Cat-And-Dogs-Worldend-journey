using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawner : Singleton<BossEnemySpawner>
{
    [Tooltip("生成的怪物列表")]
    public List<GameObject> enemyToSpawn;
    
    [Tooltip("地图的左右边界")]
    public float leftBorder;
    public float rightBorder;

    [Tooltip("怪物生成的上下边界")] 
    public float upBorder;
    public float downBorder;

    public IEnumerator SpawnEnemy(int spawnCount)
    {
        for (var i = 0; i < spawnCount; i++)
        {
            var spawnCoord = new Vector3(Random.Range(leftBorder, rightBorder),
                Random.Range(upBorder, downBorder), 0);
            // 在左右边界间尝试生成怪物
            var enemy = Instantiate(enemyToSpawn[Random.Range(0, enemyToSpawn.Count)],
                spawnCoord, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
