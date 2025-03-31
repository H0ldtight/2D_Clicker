using System;
using System.Collections.Generic;
using UnityEngine;

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
    public UpgradeOption(float value, int level, int requireGold, StatType statType)
    {
        this.value = value;
        this.level = level;
        this.requireGold = requireGold;
        this.statType = statType;
    }

    //레벨업 시 증가량
    public float value;

    //현재 레벨
    public int level;
    
    //요구골드
    public int requireGold;

    //증가시켜줄 스텟
    public StatType statType;
}

public class Character
{
    //능력치
    public StatData statData;
    //업그레이드 가능한 옵션 딕셔너리<옵션, 증가시켜줄 스텟>
    public Dictionary<UpgradeType, UpgradeOption> upgradeOptions;

    //재화
    public int point;
    public int gold;
    
    //생성자
    public Character(StatData statData)
    {
        point = 0;
        gold = 0;
        
        this.statData = statData;

        upgradeOptions = new Dictionary<UpgradeType, UpgradeOption>();

        //골드획득량증가 옵션 등록
        upgradeOptions.Add(UpgradeType.PlusGold, new UpgradeOption(30, 1, 25, StatType.ExtraGold));
        //자동공격 몇초에 1대씩 때릴건지
        upgradeOptions.Add(UpgradeType.AutoAttack, new UpgradeOption(0.2f, 1, 25, StatType.ReduceAttackSpeed));
        //크리티컬 데미지 증가
        upgradeOptions.Add(UpgradeType.Critical, new UpgradeOption(50, 1, 25, StatType.Criticaldamage));
    }

    //업그레이드하기
    public void Upgrade(UpgradeType type)
    {
        UpgradeOption upgrade = upgradeOptions[type];

        //보유 골드 모자랄 시 업그레이드 불가
        if (!GameManager.Instance.UseGold(upgrade.requireGold)) return;

        int idx = statData.FindStatIndex(upgrade.statType);

        statData.SetStat(idx, upgrade.value);
        upgrade.requireGold *= 2;
    }
}