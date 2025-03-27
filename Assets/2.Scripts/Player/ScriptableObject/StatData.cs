using System;
using System.Collections.Generic;
using UnityEngine;

//능력치 관련
public enum StatType
{
    AttackPerSecond,
    CriticalPercent,
    ExtraGold,
    AttackPower
}

[CreateAssetMenu(fileName = "NewStatBase", menuName ="Stats")]
public class StatData : ScriptableObject
{
    public Dictionary<StatType, float> stats;
}