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
    public int finalGoldBonus; // 최종 골드 보너스

    public bool isPaused; // 일시정지

    private Coroutine autoAttackCorutine; //코루틴 예외처리
    public float autoAttackSpeed = 10f; // 기본 자동 공격 시간
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
    }

    public void CalculateFinalStats()
    {
        if (WeaponManager.Instance.EquipedWeapon == null)
        {
            Debug.LogWarning("무기가 장착되지 않았습니다.");
            return;
        }
        finalAttackPower += WeaponManager.Instance.EquipedWeapon.weaponDamage;
        finalCritDamage = finalAttackPower * (int)player.statData.GetStatValue(StatType.Criticaldamage); // 무기 공격력에서 곱해줌
        finalGoldBonus = (int)player.statData.GetStatValue(StatType.ExtraGold);
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

    /// <summary>
    /// 자동 공격 업그레이드 부분
    /// </summary>
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
            float _critChance = WeaponManager.Instance.EquipedWeapon.criticalPercentage;
            if (random < _critChance) //크리티컬 데미지
            {
                target.TakeDamage(finalCritDamage);
                SoundManager.Instance.ApplyCriticalSFX();
            }
            else //일반 공격
            {
                target.TakeDamage(finalAttackPower);
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
        CalculateFinalStats();
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

    //새로 시작하기
    public void NewPlayerData()
    {
        StageManager.Instance.LoadStageFromSave(0);
        StatData statDataCopy = ScriptableObject.Instantiate(statData);
        player = new Character(statDataCopy);
        gold = 0;
        point = 0;
        autoAttackSpeed = 10f;
        UpgradeAutoAttackSpeed();


        SaveData(); //새로하기를 클릭하면 중간에 게임을 나왔다가 이어하기를 해도 다시 초기화되도록 만듬, 즉 저장하기 버튼과 이어하기만을 눌러야지 이어하기가 가능
        WeaponManager.Instance.SetEquippedWeapons();
        UIManager.Instance.MainUI.WeaponUI.UpdateUI();
    }

    //데이터 저장하기
    public void SaveData()
    {
        player.point = point;
        player.gold = gold;
        //데이터 저장
        string filePath = Path.Combine(saveDirectory, "PlayerData.json");//파일 경로 및 파일 이름 지정
        string json = JsonUtility.ToJson(player, true); //제이슨 변환 
        File.WriteAllText(filePath, json); // 생성

        SaveStageIndex();
    }

    //데이터 불러오기
    public void LoadPlayerData()
    {
        string filePath = Path.Combine(saveDirectory, "PlayerData.json"); //파일 경로

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("There is no SaveFile.");
            NewPlayerData();
            return;
        }
        string json = File.ReadAllText(filePath);  // 읽기
        player = JsonUtility.FromJson<Character>(json); // 원래 데이터로 변환
        player.LoadValue();
        gold = player.gold;
        point = player.point;
        UpgradeAutoAttackSpeed();
        CalculateFinalStats();
        UIManager.Instance.MainUI.WeaponUI.UpdateUI();
        LoadStageIndex();
    }

    public void SaveStageIndex()
    {
        string path = Path.Combine(saveDirectory, "StageData.json");
        int currentStage = StageManager.Instance.CurrentStageIndex;
        File.WriteAllText(path, currentStage.ToString());
        Debug.Log($"[스테이지 저장 완료] {currentStage}");
    }

    public void LoadStageIndex()
    {
        string path = Path.Combine(saveDirectory, "StageData.json");

        if (File.Exists(path))
        {
            string content = File.ReadAllText(path);
            if (int.TryParse(content, out int savedStage))
            {
                StageManager.Instance.LoadStageFromSave(savedStage);
                Debug.Log($"[스테이지 불러오기] {savedStage}");
            }
            else
            {
                Debug.LogWarning("StageData.json 파싱 실패. 0 스테이지로 초기화");
                StageManager.Instance.LoadStageFromSave(0);
            }
        }
        else
        {
            Debug.LogWarning("StageData.json 없음. 0 스테이지로 초기화");
            StageManager.Instance.LoadStageFromSave(0);
        }
    }

}