using UnityEngine;
using UnityEngine.UI;
// 테스트용
using System.Collections;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public Image healthBar;
    
    private int currentHealth;
    
    // 외부 읽기전용, 적의 체력
    public int CurrentHealth => currentHealth;
    public int MaxHealth => data.maxHealth;
    
    [SerializeField] private Image enemyImage;
    [SerializeField] private float damageFlashDuration = 0.1f;


    private void Start()
    {
        currentHealth = MaxHealth;

        if (enemyImage != null && data.enemySprite != null)
        {
            enemyImage.sprite = data.enemySprite;
        }
        
        // 테스트용
        StartCoroutine(AutoDamage());
    }

    private void TakeDamage(int damage)
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
        
        Debug.Log($"Taking Damage, Enemy health {currentHealth}/{MaxHealth}");
    }
    
    private void UpdatehealthBar()
    {
        if (healthBar != null)
        {
            float percentage = (float)currentHealth / MaxHealth;
            healthBar.fillAmount = percentage;
        }
        
    }
    
    // 피격 효과 - 흔들림
    private IEnumerator FlashDamage()
    {
        if (enemyImage != null)
        {
            Vector3 originalPos = enemyImage.rectTransform.localPosition;
            
            float elapsed = 0f;
            float duration = damageFlashDuration;
            float intensity = 5f; // 흔들림 정도

            while (elapsed < duration)
            {
                float offsetX = Random.Range(-1f, 1f) * intensity;
                float offsetY = Random.Range(-1f, 1f) * intensity;
                enemyImage.rectTransform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);
                elapsed += Time.deltaTime;
                yield return null;
            }

            enemyImage.rectTransform.localPosition = originalPos;
        }
    }

    
    // 테스트용
    private IEnumerator AutoDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            TakeDamage(20);
        }
    }
}
