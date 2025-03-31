using UnityEngine;
using UnityEngine.UI;
// 테스트용
using System.Collections;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public Image healthBar;
    
    private int currentHealth;

    private void Start()
    {
        currentHealth = data.maxHealth;
        // 테스트용
        StartCoroutine(AutoDamage());
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            EnemyManager.Instance.OnEnemyDied(this); // 자동공격, 클릭공격 두가지임 OnClick 으로 공격이들어감.
        }
        else
        {
            UpdatehealthBar();
        }
        
        Debug.Log($"Taking Damage, Enemy health {currentHealth}/{data.maxHealth}");
    }
    
    private void UpdatehealthBar()
    {
        if (healthBar != null)
        {
            float percentage = (float)currentHealth / data.maxHealth;
            healthBar.fillAmount = percentage;
        }
    }
    // 피격 애니메이션 추가 가능
    
    // 테스트용
    private IEnumerator AutoDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            TakeDamage(10);
        }
    }
}
