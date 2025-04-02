using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeaponDataManager : MonoBehaviour
{
    public static WeaponDataManager Instance { get; private set; }
    static string folderPath => Path.Combine(Application.dataPath, "7.WeaponData");
    static string fileName = "WeaponData.csv";
    string filePath => Path.Combine(folderPath, fileName);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시에도 객체가 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);  // 이미 인스턴스가 존재하면 파괴
        }
        Create();
    }
    public void Create()
    {
        Debug.Log("Create 실행.");
        if (!Directory.Exists(folderPath))
        {
            Debug.Log("폴더가 없습니다. 새로 생성합니다.");
            Directory.CreateDirectory(folderPath);
        }

        if (!File.Exists(filePath))
        {
            Debug.Log("CSV 파일이 없습니다. 새로 생성합니다.");
            CreateDefaultCsv();
        }

        List<WeaponData> weapons = LoadWeapons();
        // UI와 연결해서 무기 리스트 전달
        StartCoroutine(DelayedSetWeapons(weapons));
    }

    private IEnumerator DelayedSetWeapons(List<WeaponData> weapons)
    {
        // WeaponUI2 인스턴스가 초기화될 때까지 기다린다.
        yield return new WaitUntil(() => WeaponUI2.Instance != null);
        WeaponUI2.Instance.SetWeapons(weapons);
    }
    public void CreateDefaultCsv()
    {
        List<WeaponData> defaultWeapons = new List<WeaponData>
        {
            // 이름, 아이콘파일이름, Lv, DMG, Crit, Crit증가, 업그레이드 비용 증가, 데미지 증가, 초기 구매비용, 초기 업그레이드 비용, 구매여부, 장착여부
            CreateWeaponData("나무검", "woodsword.png", 1, 1, 5.00f, 0.05f, 10, 1, 10, 10, true, true),
            CreateWeaponData("돌검", "stonesword.png", 1, 5, 10.00f, 0.10f, 20, 2, 20, 20, false, false),
            CreateWeaponData("철검", "ironsword.png", 1, 10, 15.00f, 0.20f, 40, 4, 40, 40, false, false),
            CreateWeaponData("황금검", "goldensword.png", 1, 20, 20.00f, 0.4f, 80, 10, 80, 80, false, false),
            CreateWeaponData("다이아몬드검", "diamondsword.png", 1, 50, 25.00f, 1.00f, 100, 20, 100, 100, false, false)

        };

        SaveWeapons(defaultWeapons);
    }

    // 무기 데이터 생성
    public WeaponData CreateWeaponData(
        string weaponName,
        string icon,
        int upgradeLevel,
        int weaponDamage,
        float criticalPercentage,
        float incresedCriticalPercentage,
        int increasedCost,
        int incresedDamage,
        int purchaseCost,
        int upgradeCost,
        bool isPurchased,
        bool isEquiped)
    {
        WeaponData weapon = ScriptableObject.CreateInstance<WeaponData>();
        weapon.Initialize(
            weaponName,
            icon,
            upgradeLevel,
            weaponDamage,
            criticalPercentage,
            incresedCriticalPercentage,
            increasedCost,
            incresedDamage,
            purchaseCost,
            upgradeCost,
            isPurchased,
            isEquiped);

        return weapon;
    }

    // CSV에서 모든 무기 불러오기
    public List<WeaponData> LoadWeapons()
    {
        List<WeaponData> weapons = new List<WeaponData>();

        if (!File.Exists(filePath))
        {
            Debug.LogError("CSV 파일이 존재하지 않습니다.");
            return weapons;
        }

        string[] lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(',');

            if (data.Length == 12)
            {
                // CSV에서 아이콘 파일명 호출
                string iconFileName = data[1];

                // WeaponData 객체 초기화
                WeaponData weapon = ScriptableObject.CreateInstance<WeaponData>();
                weapon.Initialize(
                    data[0],                          // weaponName (string)
                    data[1],                          // icon (string)
                    int.Parse(data[2]),               // upgradeLevel (int)
                    int.Parse(data[3]),               // weaponDamage (int)
                    float.Parse(data[4]),             // criticalPercentage (float)
                    float.Parse(data[5]),             // incresedCriticalPercentage (float)
                    int.Parse(data[6]),               // increasedCost (int)
                    int.Parse(data[7]),               // incresedDamage (int)
                    int.Parse(data[8]),               // purchaseCost (int)
                    int.Parse(data[9]),               // upgradeCost (int)
                    bool.Parse(data[10]),             // isPurchased (bool)
                    bool.Parse(data[11])              // isEquiped (bool)
                );
                weapons.Add(weapon);  // 무기 데이터 불러오기
            }
        }

        return weapons;
    }

    // CSV 파일 저장 (업데이트)
    public void SaveWeapons(List<WeaponData> weapons)
    {
        List<string> lines = new List<string>
        {
        "weaponName,icon,upgradeLevel,weaponDamage,criticalPercentage,incresedCriticalPercentage,increasedCost,incresedDamage,purchaseCost,upgradeCost,IsPurchased,IsEquiped"
        };

        foreach (var weapon in weapons)
        {
            lines.Add(weapon.ToCsvString());
        }

        File.WriteAllLines(filePath, lines);
        Debug.Log("무기 데이터가 CSV 파일에 저장되었습니다.");
    }
}
