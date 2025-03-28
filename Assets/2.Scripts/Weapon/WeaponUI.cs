using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [SerializeField]
    public WeaponData weaponData;

    public GameObject WeaponWindow;
    public GameObject WeaponInventory;

    void Start()
    {
        WeaponInventory.SetActive(false);
    }

    public void SetWeaponData(WeaponData weponData)
    {
        weaponData = weponData;
    }




}
