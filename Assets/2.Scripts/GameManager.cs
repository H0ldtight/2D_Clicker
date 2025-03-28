using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //플레이어 데이터
    public Character player;

    public float gold; //총 골드
    public float point; // 총 포인트

    public float finalAttackPower; //최종 데미지
    public float finalCritDamage; // 최종 크리티컬 데미지
    public float finalGoldBonus = 1; // 최종 골드 보너스

    public bool isPaused; // 일시정지

    private Coroutine autoAttackCorutine; //코루틴 예외처리
    public float autoAttackSpeed = 3f; // 기본 자동 공격 시간
    public WaitForSeconds autoAttackDelay;  // 자동공격 코루틴 딜레이 재생성 방지

    private string saveDirectory;//저장 경로


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        saveDirectory = Path.Combine(Application.dataPath, "5.PlayerData"); //에셋 저장 경로
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }
    private void Start()
    {
        autoAttackDelay = new WaitForSeconds(autoAttackSpeed);
        StartAutoAttack();
    }

    public void CalculateFinalStats()
    {
        //finalAttackPower +=
        //finalCritDamage +=
        //finalGoldBonus +=
    }

    public void SaveData()
    {
        //데이터 저장
        string filePath = Path.Combine(saveDirectory,"PlayerData.json");//파일 경로 및 파일 이름 지정
        string json = JsonUtility.ToJson(player, true); //제이슨 변환 
        File.WriteAllText(filePath, json); // 생성
    }
    public void NewPlayerData()
    {
        //플레이어 데이터 생성

    }
    public void LoadPlayerData() //이어 하기시
    {

    }

    public void StartAutoAttack() // 자동공격 시작
    {
        if (autoAttackCorutine == null)
            autoAttackCorutine = StartCoroutine(AutoAttack());
    }
    public void StopAutoAttack() // 자동공격 멈춤 일시정지할 때
    {
        if(autoAttackCorutine != null)
        {
            StopCoroutine(autoAttackCorutine);
            autoAttackCorutine = null;
        }
    }
    public void UpgradeAutoAttackSpeed(float speedReduction)// 자동공격 업그레이드
    {
        autoAttackSpeed = math.max(0.1f, autoAttackSpeed - speedReduction);
        autoAttackDelay = new WaitForSeconds(autoAttackSpeed);

        StopAutoAttack();  // 코루틴 재시작
        StartAutoAttack();
    }
    private IEnumerator AutoAttack() //딜레이
    {
        while (true)
        {
            yield return autoAttackDelay;
            if (!isPaused)
            {
                OnClick();
            }
            
        }
    }

    public void OnClick()
    {
        if(!isPaused)
        {
            gold += finalGoldBonus;
            point += 2f;
            UIManager.Instance.MainUI.UpdateUI();
        }

    }
    public bool UseGold(float usegold) // 타입을 받아오면 하나로 줄일 수 있음.
    {
        if(gold>= usegold)
        {
            gold -= usegold;
            return true;
        }
        UIManager.Instance.MainUI.OpenMessage("골드가 부족합니다.");
        return false;
    }
    public bool UsePoint(float usepoint)// 타입을 받아오면 하나로 줄일 수 있음.
    {
        if(point >= usepoint)
        {
            point -= usepoint;
            return true;
        }
        UIManager.Instance.MainUI.OpenMessage("포인트가 부족합니다.");
        return false;
    }

    public void PlayerUpgrade()
    {

    }
}
