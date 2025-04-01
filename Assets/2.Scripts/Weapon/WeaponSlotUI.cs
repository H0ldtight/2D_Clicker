using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotUI : MonoBehaviour
{
    public List<WeaponData> weapons = new List<WeaponData>();


    public static WeaponSlotUI Instance { get; private set; }

    public GameObject IsPurchased;
    public GameObject IsEquipped;
    public GameObject BuyButton;
    public TextMeshProUGUI CostPoint;
    public TextMeshProUGUI CostGold;
    public int Index;

    public int Point;
    public int Gold;

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

    private void Start()
    {
        IsPurchased.SetActive(false);
        SetWeaponSlots();
    }

    public void SetWeapons(List<WeaponData> weaponList)
    {
        weapons = weaponList;
    }

    public void SetWeaponSlots()
    {
        CostPoint.text = weapons[Index].purchaseCost.ToString();
        CostGold.text = weapons[Index].upgradeCost.ToString();
    }

    public void BuyWeapon()
    {
        //if (WeaponUI2.Instance.weapons[Index].purchaseCost >= 플레이어 point 재화)
        //{
        //    WeaponUI2.Instance.weapons[Index].purchaseCost -= Point;
        //}

        BuyButton.SetActive(false);
        IsPurchased.SetActive(true);
    }

    public void EquipWeapon()
    {
        //if((weapons[Index].isEquiped) == true)
        //{
        //    IsEquipped.SetActive(false);
        //}
        IsEquipped.SetActive(false);
    }
}
