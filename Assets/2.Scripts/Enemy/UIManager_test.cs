using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_test : MonoBehaviour
{
    public static UIManager_test Instance { get; private set; } //읽기 전용으로 만듬

    [SerializeField] private MainUI mainUi;
    // 추가
    [SerializeField] private StageUI stageUI;

    public MainUI MainUI => mainUi;
    // 추가
    public StageUI StageUI => stageUI;
    
    public GameObject mainUiObj;
    public GameObject stageUiObj;

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
    }
    public void OpenStartUi()
    {
        mainUiObj.SetActive(false);
        stageUiObj.SetActive(false);
    }

    public void OpenMainUi()
    {
        mainUiObj.SetActive(true);
        stageUiObj.SetActive(true);
    }
}
