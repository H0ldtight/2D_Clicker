using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } //읽기 전용으로 만듬

    [SerializeField] private MainUI mainUi;
    [SerializeField] private StageUI stageUI;

    public MainUI MainUI => mainUi;
    public StageUI StageUI => stageUI;




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

}