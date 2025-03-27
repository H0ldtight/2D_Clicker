using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } //읽기 전용으로 만듬

    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private TextMeshProUGUI WarningMessageText; //경고 메세지


    [SerializeField] private GameObject WarningMessage; //경고메세지 오브젝트

    private WaitForSeconds delay = new WaitForSeconds(1f); // 경고 메세지 딜레이
    Coroutine warningCoroutine; // 코루틴 중복 방지용도
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
        WarningMessage.SetActive(false);
    }

    public void UpdateUI()
    {
        goldText.text = GameManager.Instance.gold.ToString();
        gemText.text = GameManager.Instance.gem.ToString();
    }

    public void OpenWarningMessage(string message)
    {

        WarningMessageText.text = $"{message}가 부족합니다.";
        if (warningCoroutine != null) //코루틴 예외처리
        {
            StopCoroutine(warningCoroutine);
            warningCoroutine = null;
        }
           
        warningCoroutine = StartCoroutine(WarningmessageDelay());


    }

    IEnumerator WarningmessageDelay()
    {
        WarningMessage.SetActive(true);
        yield return delay;
        WarningMessage.SetActive(false);
        warningCoroutine = null;
    }
}
