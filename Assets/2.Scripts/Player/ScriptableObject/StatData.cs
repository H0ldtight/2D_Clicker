using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

//능력치 관련
[System.Serializable]
public enum StatType
{
    ReduceAttackSpeed,    //초당공격
    Criticaldamage,     //크리티컬 데미지
    ExtraGold,          //증가한 골드획득량
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

//데이터 직렬화용 StatData
[Serializable]
public class SerializableStatData
{
    public List<Stat> stats = new List<Stat>();
}


[CreateAssetMenu(fileName = "NewStatBase", menuName ="Stats")]
public class StatData : ScriptableObject
{
    public Stat[] stats;

    private void OnValidate()
    {
        if (stats.Length <= (int)StatType.Count)
        {
            Make();
        }
    }

    public void Make()
    {
        stats = new Stat[(int)StatType.Count];
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = new Stat((StatType)i);
        }
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

    public float GetStatValue(StatType type)
    {
        return stats[FindStatIndex(type)].totalValue;
    }

    public void SetStat(int idx, float value)
    {
        stats[idx].totalValue += value;
    }
}