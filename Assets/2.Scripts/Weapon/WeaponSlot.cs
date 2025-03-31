using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WeaponSlot : MonoBehaviour
{
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponLv;
    public TextMeshProUGUI weaponAtk;
    public TextMeshProUGUI weaponCrit;
    public Image icon;

    public TextMeshProUGUI purchaseCost; // Point
    public TextMeshProUGUI upgradeCost; // Gold

    public bool isPurchased = false;
    public bool isEquiped = false;

    public int Index = 0;

    
    public void SetWeaponInventoryData()
    {

        List<WeaponData> weapons = WeaponUI2.Instance.Weapons;

        WeaponData weapon = weapons[Index];

        // UI 업데이트
        weaponName.text = weapon.weaponName;
        weaponLv.text = weapon.upgradeLevel.ToString();
        weaponAtk.text = weapon.weaponDamage.ToString();
        weaponCrit.text = weapon.criticalPercentage.ToString("N2") + "%";
        purchaseCost.text = weapon.purchaseCost.ToString();
        upgradeCost.text = weapon.upgradeCost.ToString();
        icon.sprite = weapon.icon;

        // 구매 및 장착 상태 반영
        isPurchased = weapon.isPurchased;
        isEquiped = weapon.isEquiped;
    }
}
