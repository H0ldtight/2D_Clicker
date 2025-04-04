using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private Button startBotton; // 시작 버튼
    [SerializeField] private Button continueButton; // 이어하기 버튼
    [SerializeField] private Button settingSoundButton; // 음악 설정 버튼
    [SerializeField] private Button soundBackBtn; //음악 설정 뒤로가기 버튼

    private void Start()
    {
        startBotton.onClick.AddListener(() => OnClickStart());
        continueButton.onClick.AddListener(() => OnClickContinue()); 
        settingSoundButton.onClick.AddListener(()=>UIManager.Instance.OpenSoundUi());
        soundBackBtn.onClick.AddListener(() => UIManager.Instance.CloseSoundUi());
    }

    public void OnClickStart()
    {
        UIManager.Instance.OpenMainUi();
        WeaponDataManager.Instance.NewWeaponData();
        GameManager.Instance.NewPlayerData();
        UIManager.Instance.MainUI.UpdateUI();
    }
    public void OnClickContinue()
    {
        WeaponDataManager.Instance.ContinueWeaponData();
        UIManager.Instance.OpenMainUi();
        GameManager.Instance.LoadPlayerData();
    }


}
