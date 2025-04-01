using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public WeaponDataManager weaponDataManager = new WeaponDataManager();

    //플레이어 데이터
    public Character player;
    public StatData statData;

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

    public GameObject clickEffectPrefab; //파티클 프리펩
    public Transform effectHolder; //파티클 

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
        weaponDataManager.Create();
    }

    public void CalculateFinalStats()
    {
        //finalAttackPower +=
        //finalCritDamage +=
        //finalGoldBonus +=
    }

    public void SaveData()
    {
        player.point = (int)point;
        player.gold = (int)gold;
        //데이터 저장
        string filePath = Path.Combine(saveDirectory,"PlayerData.json");//파일 경로 및 파일 이름 지정
        string json = JsonUtility.ToJson(player, true); //제이슨 변환 
        File.WriteAllText(filePath, json); // 생성
    }
    public void NewPlayerData() // 새로 시작
    {
        //플레이어 데이터 생성
        player = new Character(statData);
        Debug.Log(player);
    }

    public void LoadPlayerData() //이어 하기시
    {
        string filePath = Path.Combine(saveDirectory, "PlayerData.json"); //파일 경로
        string json = File.ReadAllText(filePath);  // 읽기
        Character player = JsonUtility.FromJson<Character>(json); // 원래 데이터로 변환
        //Debug.Log(player.gold);
        //Debug.Log(player.point);
        gold = player.gold;
        point = player.point;

    }

    public void OnClick()
    {
        if(!isPaused)
        {
            gold += finalGoldBonus;
            point += 2f;
            UIManager.Instance.MainUI.UpdateUI();

            SpawnClickEffect(Input.mousePosition); // 클릭 이펙트 여기서 실행!
        }
    }

    public void SpawnClickEffect(Vector3 screenPosition) //파티클 시스템
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPos.z = 0f;

        GameObject fx = Instantiate(clickEffectPrefab, worldPos, Quaternion.identity, effectHolder);
        Destroy(fx, 1f);
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

    // 자동공격 업그레이드
    public void UpgradeAutoAttackSpeed()
    {
        player.Upgrade(UpgradeType.AutoAttack);
        float speedReduction = player.statData.GetStatValue(StatType.ReduceAttackSpeed);

        autoAttackSpeed = math.max(0.1f, autoAttackSpeed - speedReduction);
        autoAttackDelay = new WaitForSeconds(autoAttackSpeed);

        StopAutoAttack();  // 코루틴 재시작
        StartAutoAttack();
    }

    public void StartAutoAttack() // 자동공격 시작
    {
        if (autoAttackCorutine == null)
            autoAttackCorutine = StartCoroutine(AutoAttack());
    }

    public void StopAutoAttack() // 자동공격 멈춤 일시정지할 때
    {
        if (autoAttackCorutine != null)
        {
            StopCoroutine(autoAttackCorutine);
            autoAttackCorutine = null;
        }
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

    //크리티컬 업그레이드
    public void UpgradeCriticalDamage()
    {
        player.Upgrade(UpgradeType.Critical);
        UIManager.Instance.MainUI.UpdateUI();
    }

    //골드 획득량 증가 업그레이드
    public void UpgradeMoreMoney()
    {
        player.Upgrade(UpgradeType.PlusGold);
        UIManager.Instance.MainUI.UpdateUI();
    }

}