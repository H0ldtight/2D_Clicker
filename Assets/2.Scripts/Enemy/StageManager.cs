using System.Collections.Generic;
using UnityEngine;

// 게임의 진행 흐름, 난이도 및 보상 세팅
public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public EnemyData enemyTemplate;
    
    private int currentStageIndex;
    
    // 외부 읽기전용, 최근 스테이지
    public int CurrentStageIndex => currentStageIndex;
    
    [Header("Enemy Setting")]
    [SerializeField] private int baseHealth = 50;
    [SerializeField] private int baseCount = 10;
    [SerializeField] private int healthPerStage = 10;
    [SerializeField] private int countPerStage = 1;
    
    [Header("Reward Settings")]
    [SerializeField] private int baseGoldReward = 1;
    [SerializeField] private int goldPerStage = 2; // 스테이지마다 골드 보상 증가
    [SerializeField] private int basePointReward = 1;
    [SerializeField] private int pointPerStage = 1; // 스테이지마다 포인트 보상 증가

    [Header("Enemy Image Pool")]
    [SerializeField] private string enemySpriteFolderPath = "EnemySprites";
    private Sprite[] allEnemySprites;

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
        allEnemySprites = Resources.LoadAll<Sprite>(enemySpriteFolderPath);
        StartStage(currentStageIndex);
    }

    public void StartStage(int stageIndex)
    {
        UIManager.Instance.StageUI.UpdateStageText(currentStageIndex);
        currentStageIndex = stageIndex;

        EnemyData stageEnemy = ScriptableObject.CreateInstance<EnemyData>();
        stageEnemy.enemyName = enemyTemplate.enemyName + $"{stageIndex + 1}";
        // 스테이지 난이도 - 스테이지 올라갈수록 적의 체력,개수 증가
        stageEnemy.maxHealth = baseHealth + healthPerStage * stageIndex;
        stageEnemy.enemyCount = baseCount + countPerStage * stageIndex;

        // 보상 - 스테이지 올라갈수록 받는 골드, 포인트 증가 // 적 클릭 골드, 적 죽이면 포인트
        int goldReward = baseGoldReward + goldPerStage * stageIndex;
        int pointReward = basePointReward + pointPerStage * stageIndex;
        
        // UI
        UIManager.Instance.StageUI.SetStage(currentStageIndex);
        UIManager.Instance.StageUI.SetEnemy();
        
        if (allEnemySprites != null && allEnemySprites.Length > 0)
        {
            stageEnemy.enemySprite = allEnemySprites[Random.Range(0, allEnemySprites.Length)];
        }
        
        EnemyManager.Instance.SpawnEnemy(stageEnemy);
        
        Debug.Log("Stage start!");
    }

    public void OnStageCleared()
    {
        currentStageIndex++;
        
        Debug.Log("Stage cleared!");
        StartStage(currentStageIndex);
    }
}
