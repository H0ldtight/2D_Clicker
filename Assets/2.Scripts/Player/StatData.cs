using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    AttackDamage,
    CriticalPercent,
    CriticalDamage,
    GoldPerAction,
    AutoAttack,
}

[Serializable]
public struct Stat
{
    public StatType type;
    public string value;
}

[CreateAssetMenu(fileName = "NewStatBase", menuName ="Stats")]
public class StatData : ScriptableObject
{
    public List<Stat> stats;
}



