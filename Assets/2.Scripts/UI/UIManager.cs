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
    [SerializeField] private StageUI stageUI;

    public MainUI MainUI => mainUi;
    public StageUI StageUI => stageUI;

    public GameObject startUiObj;
    public GameObject mainUiObj;
    public GameObject weaponInventoryUiObj;
    public GameObject pausedUiObj;
    public GameObject stageUiObj;
    public GameObject soundUiObj;
    public GameObject enemyObj;

    public Button pausedBtn;
    public bool togle; //일시 정지 토글용



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
    /// <summary>
    /// 시작 화면
    /// </summary>
    public void OpenStartUi() 

    {
        FadeManager.Instance.FadeIn(); // 화면 전환 효과
        startUiObj.SetActive(true);
        enemyObj.SetActive(false);
        mainUiObj.SetActive(false);
        pausedUiObj.SetActive(false);
        stageUiObj.SetActive(false);
        foreach (Transform child in enemyObj.transform) //적 중복 생성 방지
        {
            GameObject.Destroy(child.gameObject);
        }
        togle = false; 
        GameManager.Instance.isPaused = true; //일시 정지 
        SoundManager.Instance.ApplyBGM(BGMType.Start);
        SoundManager.Instance.ApplyStartSceneSFX();
    }
    /// <summary>
    /// 게임 시작 화면
    /// </summary>
    public void OpenMainUi() 
    {
        GameManager.Instance.StartAutoAttack();
        FadeManager.Instance.FadeIn(); // 화면 전환 효과
        mainUiObj.SetActive(true);
        enemyObj.SetActive(true);
        stageUiObj.SetActive(true);
        startUiObj.SetActive(false);
        GameManager.Instance.isPaused = false; // 일시 정지 해제
        SoundManager.Instance.ApplyBGM(BGMType.Main);
        SoundManager.Instance.ApplyMainSceneSFX();
    }

    public void TogglePausedUi() //일시 정지 화면
    {

        GameManager.Instance.isPaused = !GameManager.Instance.isPaused;
        togle = !togle;
        pausedUiObj.SetActive(togle);
        enemyObj.SetActive(!togle);
    }
    public void OpenWeaponInventoryUi() // 장비 인벤토리 화면 // WeaponUI에서 구현중
    {
        weaponInventoryUiObj.SetActive(true);
    }
    /// <summary>
    /// 사운드 옵션 On
    /// </summary>
    public void OpenSoundUi()
    {
        soundUiObj.SetActive(true);

    }
    /// <summary>
    /// 사운도 옵션 off
    /// </summary>
    public void CloseSoundUi()
    {
        soundUiObj.SetActive(false);
    }

}

