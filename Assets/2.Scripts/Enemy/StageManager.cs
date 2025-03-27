using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임의 진행 흐름
public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public List<EnemyData> stageEnemies;
    
    private int currentStageIndex = 0;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartStage(currentStageIndex);
    }

    public void StartStage(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stageEnemies.Count)
        {
            Debug.LogWarning("Invalid stage index");
            return;
        }

        currentStageIndex = stageIndex;
        EnemyData data = stageEnemies[stageIndex];
        EnemyManager.Instance.SpawnEnemy(data);
    }

    public void OnStageCleared()
    {
        currentStageIndex++;
        if (currentStageIndex < stageEnemies.Count)
        {
            StartStage(currentStageIndex);
        }
        else
        {
            Debug.Log("Stage cleared!");
        }
    }
}
