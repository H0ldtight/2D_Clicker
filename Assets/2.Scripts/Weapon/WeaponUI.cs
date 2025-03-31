using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class WeaponUI : MonoBehaviour
{

    public static WeaponUI Instance { get; private set; }
    public List<WeaponData> Weapons { get; private set; } = new List<WeaponData>();

    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponLv;
    public TextMeshProUGUI weaponAtk;
    public TextMeshProUGUI weaponCrit;
    public Image icon;

    public GameObject WeaponInventory;

    private void Awake()
    {
        // 싱글턴 인스턴스 설정
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        WeaponInventory.SetActive(false);
        Weapons = new List<WeaponData>();
    }

    public void SetWeaponData(WeaponData weapon)
    {
        weaponName.text = weapon.weaponName;
        weaponLv.text = weapon.upgradeLevel.ToString();
        icon.sprite = weapon.icon;
        weaponAtk.text = weapon.weaponDamage.ToString();
        weaponCrit.text = weapon.criticalPercentage.ToString("N2") + "%";
    }

    public void SetPlayerWeaponData(List<WeaponData> weaponList)
    {
        foreach (var weapon in weaponList)
        {
            if (weapon.isPurchased && weapon.isEquiped)
            {
                SetWeaponData(weapon);
            }
        }
    }

    public void UpdateAllWeaponSlots()
    {
        // 모든 WeaponSlot의 데이터를 갱신
        var weaponSlots = WeaponInventory.GetComponentsInChildren<WeaponSlot>();
        foreach (var slot in weaponSlots)
        {
            slot.SetWeaponInventoryData();
        }
    }

    public void WeaponInventoryPopUp()
    {
        // WeaponInventory 활성화
        WeaponInventory.SetActive(true);
    }

    public void WeaponInventoryClose()
    {
        // WeaponInventory 비활성화
        WeaponInventory.SetActive(false);
    }
}
