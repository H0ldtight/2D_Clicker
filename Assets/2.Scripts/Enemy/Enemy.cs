using UnityEngine;
using UnityEngine.UI;
// 테스트용
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public Transform hpFill; // 체력바 스케일 값 조정 용도
    public float fullWidth = 1f; // 체력바 원래 너비

    private int currentHealth;
    
    // 외부 읽기전용, 적의 체력
    public int CurrentHealth => currentHealth;
    public int MaxHealth => data.maxHealth;
    
    [SerializeField] private SpriteRenderer enemyImage;
    [SerializeField] private float damageFlashDuration = 0.1f;


    private void Start()
    {
        currentHealth = MaxHealth;

        if (enemyImage != null && data.enemySprite != null)
        {
            enemyImage.sprite = data.enemySprite;
        }
        UpdatehealthBar();
        // // 테스트용
        // StartCoroutine(AutoDamage());
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(FlashDamage());
        
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // 음수 체력 삭제
        
        if (currentHealth <= 0)
        {
            EnemyManager.Instance.OnEnemyDied(this);
        }
        else
        {
            UpdatehealthBar();
        }
        
        // Debug.Log($"Taking Damage, Enemy health {currentHealth}/{MaxHealth}");
    }

    private void UpdatehealthBar()
    {
        float ratio = (float)currentHealth / MaxHealth;
        ratio = Mathf.Clamp01(ratio); // 0~1 범위로 제한

        // 비율에 따라 가로 스케일 조절
        hpFill.localScale = new Vector3(ratio, 0.5f, 1f);

        // 왼쪽 기준처럼 보이도록 위치 조정
        hpFill.localPosition = new Vector3(-0.5f * fullWidth * (1 - ratio), 0f, 0f);
    }

    // 피격 효과 - 흔들림
    private IEnumerator FlashDamage()
    {
        if (enemyImage != null)
        {
            Vector3 originalPos = enemyImage.transform.localPosition;
            
            float elapsed = 0f;
            float duration = damageFlashDuration;
            float intensity = 0.3f; // 흔들림 정도

            while (elapsed < duration)
            {
                float offsetX = Random.Range(-1f, 1f) * intensity;
                float offsetY = Random.Range(-1f, 1f) * intensity;
                enemyImage.transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);
                elapsed += Time.deltaTime;
                yield return null;
            }

            enemyImage.transform.localPosition = originalPos;
        }
    }

    
    // // 테스트용
    // private IEnumerator AutoDamage()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(0.1f);
    //         TakeDamage(20);
    //     }
    // }
}
