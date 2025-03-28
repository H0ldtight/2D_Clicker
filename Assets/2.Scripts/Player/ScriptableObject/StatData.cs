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
    AttackPower         //공격력
}


[System.Serializable]
public struct Stat
{
    public StatType stat;
    public float totalValue;
}

[CreateAssetMenu(fileName = "NewStatBase", menuName ="Stats")]
public class StatData : ScriptableObject
{
    public Stat[] stats;
}