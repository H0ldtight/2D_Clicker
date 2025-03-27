using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public Image healthBar;
    
    private int currentHealth;

    private void Start()
    {
        currentHealth = data.maxHealth;
    }

    public void TakeDamage(int damage)
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
}
