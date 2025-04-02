using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 강화할 수 있는 옵션
/// </summary>
public enum UpgradeType
{
    AutoAttack,
    Critical,
    PlusGold
}

/// <summary>
/// 강화할 수 있는 옵션
/// </summary>
/// <typeparam name="TKey">직렬화 할 key값</typeparam>
/// <typeparam name="TValue">직렬화 할 </typeparam>
//데이터 직렬화용 딕셔너리 클래스
[Serializable]
public class Dict<TKey, TValue>
{
    public TKey key;
    public TValue value;

    public Dict(TKey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }
}

/// <summary>
/// 업그레이드 옵션 
/// </summary>
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

    /// <summary>
    /// 레벨업 시 증가량
    /// </summary>
    public float value;

    /// <summary>
    /// 현재 레벨
    /// </summary>
    public int level;

    /// <summary>
    /// 요구골드
    /// </summary>
    public int requireGold;

    /// <summary>
    /// 증가시켜줄 스텟
    /// </summary>
    public StatType statType;
}

public class Character
{
    /// <summary>
    /// 능력치
    /// </summary>
    public StatData statData;
    /// <summary>
    /// 업그레이드 가능한 옵션 딕셔너리<옵션, 증가시켜줄 스텟>
    /// </summary>
    public Dictionary<UpgradeType, UpgradeOption> upgradeOptions = new Dictionary<UpgradeType, UpgradeOption>();

    /// <summary>
    /// 직렬화용 업그레이드 데이터, 스텟 데이터
    /// </summary>
    public List<Dict<UpgradeType,UpgradeOption>> UO;
    public SerializableStatData SD;

    /// <summary>
    /// 재화
    /// </summary>
    public int point;
    public int gold;

    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="statData">스텟정보</param>
    public Character(StatData statData)
    {
        point = 0;
        gold = 0;
        
        this.statData = statData;

        //플레이어스텟 초깃값
        for (int i = 0; i < (int)StatType.Count; i++)
        {
            this.statData.SetStat(i, 0);
        }

        Set();

        //직렬화 데이터 리스트 초기화
        foreach (UpgradeType type in Enum.GetValues(typeof(UpgradeType)))
        {
            UO.Add(new Dict<UpgradeType, UpgradeOption>(type, upgradeOptions[type]));
        }
        foreach (Stat stat in statData.stats)
        {
            SD.stats.Add(stat);
        }
    }

    /// <summary>
    /// 업그레이드 기능
    /// </summary>
    /// <param name="type">업그레이드 타입</param>
    public void Upgrade(UpgradeType type)
    {
        UpgradeOption upgrade = upgradeOptions[type];

        //보유 골드 모자랄 시 업그레이드 불가
        if (!GameManager.Instance.UseGold(upgrade.requireGold)) return;

        int idx = statData.FindStatIndex(upgrade.statType);

        //업그레이드 진행
        statData.SetStat(idx, upgrade.value);
        upgrade.level++;
        upgrade.requireGold *= 2;

        //직렬화용 데이터 업데이트
        UO[(int)type].value = upgrade;
        UIManager.Instance.MainUI.WeaponUI.UpdateUI(type);
        foreach(Stat stat in SD.stats)
        {
            stat.totalValue = statData.GetStatValue(upgrade.statType);
        }
    }

    /// <summary>
    /// 초기화 작업
    /// </summary>
    public void Set()
    {
        //딕셔너리 초기화
        upgradeOptions = new Dictionary<UpgradeType, UpgradeOption>();

        //직렬화 데이터 초기화
        UO = new List<Dict<UpgradeType, UpgradeOption>>();
        SD = new SerializableStatData();

        //골드획득량증가 옵션 등록
        upgradeOptions.Add(UpgradeType.PlusGold, new UpgradeOption(5, 0, 25, StatType.ExtraGold));
        //자동공격 몇초에 1대씩 때릴건지
        upgradeOptions.Add(UpgradeType.AutoAttack, new UpgradeOption(0.1f, 0, 25, StatType.ReduceAttackSpeed));
        //크리티컬 데미지 증가
        upgradeOptions.Add(UpgradeType.Critical, new UpgradeOption(50, 0, 25, StatType.Criticaldamage));
    }

    /// <summary>
    /// 딕셔너리와 스텟데이터에 직렬화를 전달해주는 역할
    /// </summary>
    public void LoadValue()
    {
        upgradeOptions = new Dictionary<UpgradeType, UpgradeOption>();
        statData = ScriptableObject.CreateInstance<StatData>();
        statData.Make();

        foreach (Dict<UpgradeType, UpgradeOption> dic in UO)
        {
            upgradeOptions.Add(dic.key, dic.value);
        }
        foreach (Stat stat in SD.stats)
        {
            statData.SetStat(statData.FindStatIndex(stat.stat), stat.totalValue);
        }
    }
}