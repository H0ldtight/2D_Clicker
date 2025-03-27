using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//강화할 수 있는 옵션
public enum UpgradeType
{
    AutoAttack,
    Critical,
    PlusGold
}

//업그레이드 요소
[Serializable]
public struct Upgrade
{
    public UpgradeType UpgradeType;
    public string level;
    public float requireGold;
}

public class Player
{
    //능력치
    public StatData stat;
    public List<Upgrade> upgradeOptions;

    //재화
    public int point;
    public int gold;
    
    public Player()
    {
        point = 0;
        gold = 0;
    }

    //public void Upgrade(UpgradeType type)
    //{
    //    if (gold < RequireGold)
    //    switch(type)
    //    {
    //        case UpgradeType.AutoAttack:
    //            {
    //                stat.stats[] += 
    //                break;
    //            }
    //        case UpgradeType.Critical:
    //            {
    //                break;
    //            }
    //        case UpgradeType.PlusGold:
    //            {
    //                break;
    //            }
    //    }
    //}
}