using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{


    public TextMeshProUGUI Text_Name_Lv;
    public TextMeshProUGUI Text_ATK;
    public TextMeshProUGUI Text_Critical;
    public TextMeshProUGUI Text_UpgradeCost;
    public TextMeshProUGUI Text_PurchaseCost;
    public Image Icon;

    private WeaponData currentWeapon;

    public void SetWeapon(WeaponData weapon)
    {
        currentWeapon = weapon;
        Text_Name_Lv.text = weapon.weaponName + " Lv." + weapon.upgradeLevel.ToString();
        Text_ATK.text = weapon.weaponDamage.ToString();
        Text_Critical.text = weapon.criticalPercentage.ToString("N2") + "%";
        Icon.sprite = weapon.icon;
        Text_UpgradeCost.text = weapon.upgradeCost.ToString();
        Text_PurchaseCost.text = weapon.purchaseCost.ToString();
    }

    public void PucrchaseWeapon()
    {
        if (GameManager.Instance.UsePoint(currentWeapon.purchaseCost))
        {
            GameManager.Instance.UsePoint(currentWeapon.purchaseCost);
            gameObject.SetActive(false);
        }
    }
}
