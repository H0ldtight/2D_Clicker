using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    
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
            EnemyManager.Instance.OnEnemyDied(this);
        }
    }
    
    // 피격 애니메이션 추가 가능
}
