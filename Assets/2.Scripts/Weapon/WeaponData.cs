using System.IO;
using UnityEngine;

public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int upgradeLevel;
    public int weaponDamage;
    public float criticalPercentage;
    public Sprite icon; // Sprite 타입

    public int purchaseCost; // Point
    public int upgradeCost; // Gold

    public bool isPurchased = false;
    public bool isEquiped = false;

    // 생성자 수정 (icon을 string으로 받음)
    public void Initialize(string weaponName, int upgradeLevel, int weaponDamage, float criticalPercentage, string icon, int purchaseCost, int upgradeCost, bool isPurchased, bool isEquiped)
    {
        this.weaponName = weaponName;
        this.upgradeLevel = upgradeLevel;
        this.weaponDamage = weaponDamage;
        this.criticalPercentage = criticalPercentage;

        // ".png" 확장자 제거한 경로로 아이콘 로드
        string iconPath = $"Icons/{Path.GetFileNameWithoutExtension(icon)}";

        // 아이콘 로드
        this.icon = Resources.Load<Sprite>(iconPath);

        this.purchaseCost = purchaseCost;
        this.upgradeCost = upgradeCost;
        this.isPurchased = isPurchased;
        this.isEquiped = isEquiped;
    }

    public string ToCsvString()
    {
        return $"{weaponName},{upgradeLevel},{weaponDamage},{criticalPercentage},{icon.name},{purchaseCost},{upgradeCost},{isPurchased},{isEquiped}";
    }
}