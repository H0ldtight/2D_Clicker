using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class WeaponUI2 : MonoBehaviour
{

    public static WeaponUI2 Instance { get; private set; }

    private List<WeaponData> weapons = new List<WeaponData>();
    public List<WeaponSlot> weaponSlots = new List<WeaponSlot>();

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
    }

    public void SetWeapons(List<WeaponData> weaponList)
    {
        weapons = weaponList;
        SetEquippedWeapons();
    }
    public List<WeaponData> GetWeapons()
    {
        return weapons;
    }

    public void SetWeaponSlots()
    {
        for (int i = 0; i < weaponSlots.Count && i < weapons.Count; i++)
        {
            if (weaponSlots[i] == null)
            {
                Debug.LogWarning($"WeaponSlot at index {i} is null!");
            }
            else
            {
                weaponSlots[i].SetWeapon(weapons[i]);
            }
        }
    }

    // 무기 데이터를 갱신하는 메서드
    public void SetWeaponData(WeaponData weapon)
    {
        icon.sprite = weapon.icon;
        weaponName.text = weapon.weaponName;
        weaponLv.text = weapon.upgradeLevel.ToString();
        weaponAtk.text = weapon.weaponDamage.ToString();
        weaponCrit.text = weapon.criticalPercentage.ToString("N2") + "%";
    }

    public void SetEquippedWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].isPurchased && weapons[i].isEquiped)
            {
                SetWeaponData(weapons[i]);
            }
        }
    }


    // WeaponInventory 활성화
    public void WeaponInventoryPopUp()
    {
        WeaponInventory.SetActive(true);
        SetWeaponSlots();  // UI가 활성화된 후에 슬롯을 업데이트
    }

    // WeaponInventory 비활성화
    public void WeaponInventoryClose()
    {
        WeaponInventory.SetActive(false);
        SetEquippedWeapons();
    }
}