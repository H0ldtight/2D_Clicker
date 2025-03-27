using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 실제 적의 출현과 관리
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    
    public GameObject enemyPrefab;
    
    private EnemyData currentEnemyData;
    private int remainCount;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SpawnEnemy(EnemyData data)
    {
        currentEnemyData = data;
        remainCount = data.enemyCount;
        SpawnNextEnemy();
    }

    private void SpawnNextEnemy()
    {
        if (remainCount <= 0)
        {
            StageManager.Instance.OnStageCleared();
            return;
        }

        GameObject enemyObj = Instantiate(enemyPrefab, GetSpawnPosition(), Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.data = currentEnemyData;
        remainCount--;
    }
    
    public void OnEnemyDied(Enemy enemy)
    {
        // 적 die시 플레이어 골드 획득 : 플레이어에서 스테이지내 적의 수가 0 인경우 골드 획득.
        Destroy(enemy.gameObject);
        SpawnNextEnemy();
    }
    
    // 적 스폰 위치 지정
    private Vector3 GetSpawnPosition()
    {
        return new Vector3(0, 0, 0);
    }
}
