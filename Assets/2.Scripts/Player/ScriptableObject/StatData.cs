using System;
using System.Collections.Generic;
using UnityEngine;

//능력치 관련
[System.Serializable]
public enum StatType
{
    AttackPerSecond,    //초당공격
    Criticaldamage,     //크리티컬 데미지
    ExtraGold,          //증가한 골드획득량
    AttackPower,        //공격력
    Count
}


[System.Serializable]
public class Stat
{
    public StatType stat;
    public float totalValue;

    public Stat(StatType stat)
    {
        this.stat = stat;
    }
}

[CreateAssetMenu(fileName = "NewStatBase", menuName ="Stats")]
public class StatData : ScriptableObject
{
    public Stat[] stats;

    private void OnValidate()
    {
        stats = new Stat[(int)StatType.Count];
        //if (stats.)
    }

    public int FindStatIndex(StatType type)
    {
        for (int i = 0; i < stats.Length;i++)
        {
            if (stats[i].stat == type)
            {
                return i;
            }
        }
        return -1;
    }
}