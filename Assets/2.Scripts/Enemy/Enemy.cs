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

    private void Start()
    {
        currentHealth = MaxHealth;
        // 테스트용
        StartCoroutine(AutoDamage());
    }

    private void TakeDamage(int damage)
    {
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
    // 피격 애니메이션 추가 가능
    
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
