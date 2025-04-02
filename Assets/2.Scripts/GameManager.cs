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
    public StatData statData;

    public int autoAttackDamage = 10; // 자동 공격 데미지 Test 용도
    public int gold; //총 골드
    public int point; // 총 포인트

    public int finalAttackPower; //최종 데미지
    public int finalCritDamage; // 최종 크리티컬 데미지
    public int finalGoldBonus = 5; // 최종 골드 보너스

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
    }

    public void CalculateFinalStats()
    {
        //finalAttackPower += //무기 공격력
        //finalCritDamage = finalAttackPower * player.statData.GetStatValue(StatType.Criticaldamage);  무기 공격력에서 곱해줌
        finalGoldBonus = (int)player.statData.GetStatValue(StatType.ExtraGold);
    }

    public void SaveData()
    {
        player.point = point;
        player.gold = gold;
        //데이터 저장
        string filePath = Path.Combine(saveDirectory,"PlayerData.json");//파일 경로 및 파일 이름 지정
        string json = JsonUtility.ToJson(player, true); //제이슨 변환 
        Debug.Log(filePath);
        File.WriteAllText(filePath, json); // 생성
    }

    public void NewPlayerData()
    {
        StatData statDataCopy = ScriptableObject.Instantiate(statData);
        player = new Character(statDataCopy);
    }


    public void LoadPlayerData() //이어 하기시
    {
        string filePath = Path.Combine(saveDirectory, "PlayerData.json"); //파일 경로
        string json = File.ReadAllText(filePath);  // 읽기
        player = JsonUtility.FromJson<Character>(json); // 원래 데이터로 변환
        //Debug.Log(player.gold);
        //Debug.Log(player.point);
        player.LoadDict();
        gold = player.gold;
        point = player.point;

    }

    public void OnClick() //클릭 이벤트
    {
        PerformAttack(false);
    }

    public void SpawnClickEffect(Vector3 screenPosition) //파티클 시스템
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPos.z = 0f;

        GameObject fx = Instantiate(clickEffectPrefab, worldPos, Quaternion.identity, effectHolder);
        Destroy(fx, 1f);
    }

    public bool UseGold(int usegold) // 타입을 받아오면 하나로 줄일 수 있음.
    {
        if(gold>= usegold)
        {
            gold -= usegold;
            return true;
        }
        UIManager.Instance.MainUI.OpenMessage("골드가 부족합니다.");
        return false;
    }
    public bool UsePoint(int usepoint)// 타입을 받아오면 하나로 줄일 수 있음.
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

    public void StartAutoAttack() // 자동공격 코루틴
    {
        if (autoAttackCorutine == null)
            autoAttackCorutine = StartCoroutine(AutoAttack());
    }

    public void StopAutoAttack() // 자동공격 stop 코루틴
    {
        if (autoAttackCorutine != null)
        {
            StopCoroutine(autoAttackCorutine);
            autoAttackCorutine = null;
        }
    }
    private void AutoAttackEnemy()
    {
        PerformAttack(true);
    }

    public void PerformAttack(bool Auto = false)
    {
        Enemy target = EnemyManager.Instance.CurrentEnemy;
        if (target != null && !isPaused)
        {
            float random = UnityEngine.Random.Range(0.0f, 1.0f);
            float _critChance = 0.2f;
            if (random < _critChance) //나중 무기 크리티컬
            {
                target.TakeDamage(20);
                SoundManager.Instance.ApplyCriticalSFX();
            }
            else
            {
                target.TakeDamage(10);
                SoundManager.Instance.ApplyMainSceneSFX();
            }
            SoundManager.Instance.PlaySFX();
            gold += finalGoldBonus;
            UIManager.Instance.MainUI.UpdateUI();
            if (Auto)
            {
                GameObject fx = Instantiate(clickEffectPrefab, target.transform.position, Quaternion.identity, effectHolder);
                Destroy(fx, 1f);
            }
            else
            {
                SpawnClickEffect(Input.mousePosition); // 클릭 이펙트 여기서 실행!
            }
        }

    }
    private IEnumerator AutoAttack() //딜레이
    {
        while (true)
        {
            yield return autoAttackDelay;
            if (!isPaused)
            {
                AutoAttackEnemy();
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
        CalculateFinalStats();
    }

    public void AddPoint(int amount)
    {
        point += amount;
    }
}