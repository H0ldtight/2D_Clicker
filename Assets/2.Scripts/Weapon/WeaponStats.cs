
using Unity.Burst.CompilerServices;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WeaponData
{
    public string weaponName;
    public int upgradeLevel;
    public int weaponDamage;
    public float criticalPercentage;

    // WeaponInventory
    public int weaponPurchaseCost;      // PointCost
    public int weaponUpgradeCost;       // GoldCost

    public bool IsPurchased = false;
    public bool IsEquiped = false;

    public WeaponData(string name, int lv, int dmg, int crit, int point, int gold, bool isPurchased, bool isEquiped)
    {
        weaponName = name;
        upgradeLevel = lv;
        weaponDamage = dmg;
        criticalPercentage = crit;

        weaponPurchaseCost = point;
        weaponUpgradeCost = gold;

        IsPurchased = isPurchased;
        IsEquiped = isEquiped;
    }

}
