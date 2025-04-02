using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausedUI : MonoBehaviour
{
    [SerializeField] private Button startBotton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button saveButton;

    private void Start()
    {
        startBotton.onClick.AddListener(() => UIManager.Instance.OpenStartUi());
        continueButton.onClick.AddListener(() => UIManager.Instance.TogglePausedUi());
        saveButton.onClick.AddListener(() => SaveData());

    }
    public void SaveData()
    {
        UIManager.Instance.TogglePausedUi();
        GameManager.Instance.SaveData();
        WeaponDataManager.Instance.SaveWeapons(WeaponManager.Instance.weapons);
        UIManager.Instance.MainUI.OpenMessage("저장이 완료 되었습니다.");   
    }
}
