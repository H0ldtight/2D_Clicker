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
        // 자동공격, 클릭공격 두가지임 OnClick 으로 공격이들어감.
    }
    
    // 피격 애니메이션 추가 가능
}
