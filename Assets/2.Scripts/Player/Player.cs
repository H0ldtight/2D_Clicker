using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.AI;

//강화할 수 있는 옵션
public enum UpgradeType
{
    AutoAttack,
    Critical,
    PlusGold
}

//업그레이드 옵션 
[Serializable]
public class UpgradeOption
{
    //강화 옵션명
    public UpgradeType UpgradeType;
    //레벨업 시 증가량
    public float value;
    //현재 레벨
    public int level;
    //요구골드
    public int requireGold;
}

public class Player
{
    //능력치
    public StatData statData;
    //업그레이드 가능한 옵션 딕셔너리<옵션, 증가시켜줄 스텟>
    public Dictionary<UpgradeOption, StatType> upgradeOptions;

    //재화
    public int point;
    public int gold;
    
    //생성자
    public Player(StatData statData)
    {
        point = 0;
        gold = 0;
        this.statData = statData;
        statData.stats.Add(StatType.AttackPower, 0);
    }

    //업그레이드하기
    public void Upgrade(UpgradeOption upgrade)
    {
        //보유 골드 모자랄 시 업그레이드 불가
        if (gold < upgrade.requireGold)
        {
            // TODO: UI 나중에 연결하기
            return;
        }

        StatType stat = upgradeOptions[upgrade];

        upgrade.level++;
        this.statData.stats[stat] += upgrade.level * upgrade.value;
        upgrade.requireGold = upgrade.requireGold * 2;
    }
}