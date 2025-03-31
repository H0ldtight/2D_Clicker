using UnityEngine;

// 실제 적의 출현과 관리
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    
    public GameObject enemyPrefab;
    
    private EnemyData currentEnemyData;
    private int remainCount;
    
    // 스테이지 보상관련
    private int goldReward;
    private int pointReward;

    [SerializeField] private Transform enemyParent; // 적 프리팹의 부모
    
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
        
        Debug.Log("Spawned Enemy.");
    }

    private void SpawnNextEnemy()
    {
        if (remainCount <= 0)
        {
            StageManager.Instance.OnStageCleared();
            return;
        }

        GameObject enemyObj = Instantiate(enemyPrefab, GetSpawnPosition(), Quaternion.identity, enemyParent);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.data = currentEnemyData;
        
        remainCount--;
    }
    
    public void OnEnemyDied(Enemy enemy)
    {
        // 스테이지 보상관련
        // GameManager.Instance.AddGold(goldReward);
        // GameManager.Instance.AddPoint(pointReward);

        Destroy(enemy.gameObject);
        SpawnNextEnemy();
    }
    
    // 적 스폰 위치 지정
    private Vector3 GetSpawnPosition()
    {
        return enemyParent != null ? enemyParent.position : Vector3.zero;
    }
}
