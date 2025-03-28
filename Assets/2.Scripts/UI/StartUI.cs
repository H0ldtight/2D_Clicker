using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private Button startBotton;
    [SerializeField] private Button continueButton;


    private void Start()
    {
        startBotton.onClick.AddListener(() => OnClickStart());
        continueButton.onClick.AddListener(() => OnClickContinue());
    }

    public void OnClickStart()
    {
        GameManager.Instance.NewPlayerData();
        UIManager.Instance.OpenMainUi();
    }
    public void OnClickContinue()
    {
        GameManager.Instance.LoadPlayerData();
        UIManager.Instance.OpenMainUi();
    }
}
