using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI WarningMessageText; //경고 메세지

    [SerializeField] private GameObject WarningMessage; //경고메세지 오브젝트

    private WaitForSeconds delay = new WaitForSeconds(1f); // 경고 메세지 딜레이
    Coroutine warningCoroutine; // 코루틴 중복 방지용도
    private void Start()
    {
        WarningMessage.SetActive(false);
    }

    public void UpdateUI()
    {
        goldText.text = GameManager.Instance.gold.ToString();
        pointText.text = GameManager.Instance.point.ToString();
    }

    public void OpenMessage(string message)
    {

        WarningMessageText.text = $"{message}";
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
