using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    
    
    public TextMeshProUGUI Text_Name_Lv;
    public TextMeshProUGUI Text_ATK;
    public TextMeshProUGUI Text_Critical;
    public Image Icon;

    public void SetWeapon(WeaponData weapon)
    {
        Text_Name_Lv.text = weapon.weaponName + " Lv." + weapon.upgradeLevel.ToString();
        Text_ATK.text = weapon.weaponDamage.ToString();
        Text_Critical.text = weapon.criticalPercentage.ToString("N2") + "%";
        Icon.sprite = weapon.icon;
    }
}
