using System.IO;
using UnityEngine;

public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int upgradeLevel;
    public int weaponDamage;
    public float criticalPercentage;
    public Sprite icon; // Sprite 타입

    public int purchaseCost;            // Point
    public int upgradeCost;             // Gold

    public int increasedCost;           // 
    public int incresedDamage;          // 
    public float incresedCriticalPercentage;    // 

    public bool isPurchased = false;
    public bool isEquiped = false;

    // 생성자 수정 (icon을 string으로 받음)
    public void Initialize(
        string weaponName,
        string icon,
        int upgradeLevel,
        int weaponDamage,
        float criticalPercentage,
        float incresedCriticalPercentage,
        int increasedCost,
        int incresedDamage,
        int purchaseCost,
        int upgradeCost,
        bool isPurchased,
        bool isEquiped)
    {
        this.weaponName = weaponName;

        string iconPath = $"Icons/{Path.GetFileNameWithoutExtension(icon)}";    // ".png" 확장자 제거한 경로로 아이콘 로드
        this.icon = Resources.Load<Sprite>(iconPath);

        this.upgradeLevel = upgradeLevel;
        this.weaponDamage = weaponDamage;
        this.criticalPercentage = criticalPercentage;
        this.incresedCriticalPercentage = incresedCriticalPercentage;

        this.increasedCost = increasedCost;
        this.incresedDamage = incresedDamage;

        this.purchaseCost = purchaseCost;
        this.upgradeCost = upgradeCost;

        this.isPurchased = isPurchased;
        this.isEquiped = isEquiped;
    }

    public string ToCsvString()
    {
        return $"{weaponName},{icon.name},{upgradeLevel},{weaponDamage},{criticalPercentage},{incresedCriticalPercentage},{increasedCost},{incresedDamage},{purchaseCost},{upgradeCost},{isPurchased},{isEquiped}";
    }

}