using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int enemyCount;
    // 보상관련 추가하기

    private void Awake()
    {
        enemyName = this.name;
        maxHealth = this.maxHealth;
        enemyCount = this.enemyCount;
    }
}
