using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } //읽기 전용으로 만듬

    [SerializeField] private MainUI mainUi;

    public MainUI MainUI => mainUi;

    public GameObject startUiObj;
    public GameObject mainUiObj;
    public GameObject weaponInventoryUiObj;
    public GameObject pausedUiObj;

    public Button pausedBtn;
    public bool togle;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        OpenStartUi();
        pausedBtn.onClick.AddListener(() => TogglePausedUi());
    }
    public void OpenStartUi() // 시작 화면
    {
        startUiObj.SetActive(true);
        mainUiObj.SetActive(false);
    }

    public void OpenMainUi() // 메인 화면
    {
        mainUiObj.SetActive(true);
        startUiObj.SetActive(false);
    }

    public void TogglePausedUi() //일시 정지 화면
    {
        togle = !togle;
        pausedUiObj.SetActive(togle);
    }
    public void OpenWeaponInventoryUi() // 장비 인벤토리 화면
    {
        weaponInventoryUiObj.SetActive(true);
    }

}
