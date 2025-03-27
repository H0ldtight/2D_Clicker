using System;
using System.Collections.Generic;
using UnityEngine;

//능력치 관련
public enum StatType
{
    AttackPerSecond,    //초당공격
    Criticaldamage,     //크리티컬 데미지
    ExtraGold,          //증가한 골드획득량
    AttackPower         //공격력
}

[CreateAssetMenu(fileName = "NewStatBase", menuName ="Stats")]
public class StatData : ScriptableObject
{
    public Dictionary<StatType, float> stats;
}