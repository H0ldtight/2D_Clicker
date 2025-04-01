using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    public TextMeshProUGUI CriticalGoldTxt;
    public TextMeshProUGUI AutoAttackGoldTxt;
    public TextMeshProUGUI MoreMoneyGoldTxt;

    public TextMeshProUGUI CriticalLevel;
    public TextMeshProUGUI AutoAttackLevel;
    public TextMeshProUGUI MoreMoneyLevel;

    public TextMeshProUGUI CriticalValue;
    public TextMeshProUGUI AutoAttackValue;
    public TextMeshProUGUI MoreMoneyValue;

    public void UpdateUI()
    {
        UpdateUI(UpgradeType.PlusGold);
        UpdateUI(UpgradeType.AutoAttack);
        UpdateUI(UpgradeType.Critical);
    }

    //업그레이드 부분
    public void UpdateUI(UpgradeType type)
    {
        TextMeshProUGUI gold;
        TextMeshProUGUI level;
        TextMeshProUGUI value;

        switch (type)
        {
            case UpgradeType.PlusGold:
                gold = MoreMoneyGoldTxt;
                level = MoreMoneyLevel;
                value = MoreMoneyValue;
                break;
            case UpgradeType.AutoAttack:
                gold = AutoAttackGoldTxt;
                level = AutoAttackLevel;
                value = AutoAttackValue;
                break;
            case UpgradeType.Critical:
                gold = CriticalGoldTxt;
                level = CriticalLevel;
                value = CriticalValue;
                break;
            default:
                return;
        }
        gold.text = $"{GameManager.Instance.player.upgradeOptions[type].requireGold} G";
        level.text = $"{GameManager.Instance.player.upgradeOptions[type].level}";
        value.text = $"{GameManager.Instance.player.upgradeOptions[type].value}";
    }
}
