using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public TextMeshProUGUI Text_Name_Lv;
    public Image Icon;
    public TextMeshProUGUI Text_ATK;
    public TextMeshProUGUI Text_Critical;
    public TextMeshProUGUI Text_PurchaseCost;
    public TextMeshProUGUI Text_UpgradeCost;

    private WeaponData currentWeapon;

    public GameObject Upgrade;
    public GameObject IsPurchased;
    public GameObject isEquiped;

    public void SetWeapon(WeaponData weapon)
    {
        currentWeapon = weapon;

        UpdateWeaponUI();
        UpdatePurchaseStatus();
    }

    private void UpdateWeaponUI()
    {
        Icon.sprite = currentWeapon.icon;
        Text_Name_Lv.text = currentWeapon.weaponName + " Lv." + currentWeapon.upgradeLevel.ToString();
        Text_ATK.text = currentWeapon.weaponDamage.ToString();
        Text_Critical.text = currentWeapon.criticalPercentage.ToString("N2") + "%";
        Text_PurchaseCost.text = currentWeapon.purchaseCost.ToString();
        Text_UpgradeCost.text = currentWeapon.upgradeCost.ToString();
    }

    private void UpdatePurchaseStatus()
    {
        IsPurchased.SetActive(!currentWeapon.isPurchased);
        Upgrade.SetActive(currentWeapon.isPurchased);
        isEquiped.SetActive(!currentWeapon.isEquiped);
    }

    public void PucrchaseWeapon()
    {
        if (GameManager.Instance.UsePoint(currentWeapon.purchaseCost))
        {
            currentWeapon.isPurchased = true;
            Upgrade.SetActive(true);
            IsPurchased.SetActive(false);
        }
    }

    public void UpgradeWeapon()
    {
        if (GameManager.Instance.UsePoint(currentWeapon.upgradeCost))
        {
            currentWeapon.upgradeLevel++;
            currentWeapon.upgradeCost += currentWeapon.increasedCost;
            currentWeapon.weaponDamage += currentWeapon.incresedDamage;
            currentWeapon.criticalPercentage += currentWeapon.incresedCriticalPercentage;
            GameManager.Instance.CalculateFinalStats();
            UpdateWeaponUI();
        }
    }

    public void EquipWeapon()
    {
        
        // 모든 무기의 isEquiped를 false로 변경
        foreach (var slot in WeaponManager.Instance.weaponSlots)
        {
            slot.isEquiped.SetActive(true);
        }
        foreach (var weapon in WeaponManager.Instance.GetWeapons())
        {
            weapon.isEquiped = false;
        }
        isEquiped.SetActive(false);

        // 현재 무기만 isEquiped = true
        currentWeapon.isEquiped = true;
        isEquiped.SetActive(false);
        WeaponManager.Instance.SetEquippedWeapons();
    }
}
