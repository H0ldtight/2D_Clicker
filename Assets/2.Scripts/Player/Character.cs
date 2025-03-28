using System;
using System.Collections.Generic;

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

        //골드획득량증가 옵션 등록
        upgradeOptions.Add(UpgradeType.PlusGold, new UpgradeOption(30, 1, 25, StatType.ExtraGold));
        //자동공격 몇초에 1대씩 때릴건지
        upgradeOptions.Add(UpgradeType.AutoAttack, new UpgradeOption(0.2f, 1, 25, StatType.AttackPerSecond));
        //크리티컬 데미지 증가
        upgradeOptions.Add(UpgradeType.Critical, new UpgradeOption(50, 1, 25, StatType.Criticaldamage));
    }

    //업그레이드하기
    public void Upgrade(UpgradeType type)
    {
        UpgradeOption upgrade = upgradeOptions[type];

        //보유 골드 모자랄 시 업그레이드 불가
        if (gold < upgrade.requireGold)
        {
            // TODO: UI 나중에 연결하기
            return;
        }
        int idx = statData.FindStatIndex(upgrade.statType);
        
        Stat stat = statData.stats[idx];
        stat.totalValue += upgrade.value;
        upgrade.requireGold *= 2;
    }

    //무기 강화
    public void Enchant(float ATK)
    {
        // TODO: 예외 처리 필요
        ////보유 포인트 모자랄 시 업그레이드 불가
        //if (point < /*무기 강화시 필요골드*/)
        //{
        //    // TODO: UI 나중에 연결하기
        //    return;
        //}

        //statData.stats[StatType.AttackPower] += ATK;
    }
}