using System.Collections.Generic;
using UnityEngine;

// 게임의 진행 흐름
public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public List<EnemyData> stageEnemies;
    public EnemyData enemyTemplate;
    
    [SerializeField] private int baseHealth = 50;
    [SerializeField] private int baseCount = 10;
    [SerializeField] private int healthPerStage = 10;
    [SerializeField] private int countPerStage = 1;
    
    // 스테이지 보상관련
    [SerializeField] private int baseGoldReward = 1;
    [SerializeField] private int goldPerStage = 2; // 스테이지마다 골드 보상 증가
    [SerializeField] private int basePointReward = 1;
    [SerializeField] private int pointPerStage = 1; // 스테이지마다 포인트 보상 증가
    
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
        currentStageIndex = stageIndex;

        EnemyData stageEnemy = ScriptableObject.CreateInstance<EnemyData>();
        stageEnemy.enemyName = enemyTemplate.enemyName + $"_{stageIndex + 1}";
        stageEnemy.maxHealth = baseHealth + healthPerStage * stageIndex;
        stageEnemy.enemyCount = baseCount + countPerStage * stageIndex;

        // 스테이지 보상관련
        int goldReward = baseGoldReward + goldPerStage * stageIndex;
        int pointReward = basePointReward + pointPerStage * stageIndex;
        
        EnemyManager.Instance.SpawnEnemy(stageEnemy);
        
        Debug.Log("Stage start!");
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
        
        StartStage(currentStageIndex);
    }
}
