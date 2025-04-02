using UnityEngine;


// 실제 적의 출현과 관리
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    
    public GameObject enemyPrefab;
    
    public EnemyData currentEnemyData;
    private int remainCount;
    
    // 적처치 보상관련
    private int pointReward;

    [SerializeField] private Transform enemyParent; // 적 프리팹의 부모
    
    public Enemy currentEnemy;
    public Enemy CurrentEnemy => currentEnemy;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SpawnEnemy(EnemyData data, GameObject prefab)
    {
        currentEnemyData = data;
        remainCount = data.enemyCount;
        SpawnNextEnemy(prefab);
    }

    private void SpawnNextEnemy(GameObject prefab)
    {
        UIManager.Instance.StageUI.UpdateEnemyCount(remainCount);
        if (remainCount <= 0)
        {
            StageManager.Instance.OnStageCleared();
            return;
        }

        // 프리팹 가저오기
        GameObject enemyObj = Instantiate(enemyPrefab, GetSpawnPosition(), Quaternion.identity, enemyParent);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        // 적 데이터 가져오기
        enemy.init(currentEnemyData);
        
        currentEnemy = enemy; //현재 적 저장 게임 매니저 활용 목적
        remainCount--;

    }
    
    public void SetPointReward(int reward)
    {
        pointReward = reward;
    }
    
    public void OnEnemyDied(Enemy enemy)
    {
        if(enemy == currentEnemy)
        {
            currentEnemy = null;
        }
        // 적처치 보상관련
        GameManager.Instance.AddPoint(pointReward);

        Destroy(enemy.gameObject);
        SpawnNextEnemy(enemyPrefab);
    }
    
    // 적 스폰 위치 지정
    private Vector3 GetSpawnPosition()
    {
        return enemyParent != null ? enemyParent.position : Vector3.zero;
    }

    public int TotalCount => currentEnemyData != null ? currentEnemyData.enemyCount : 0;
}
