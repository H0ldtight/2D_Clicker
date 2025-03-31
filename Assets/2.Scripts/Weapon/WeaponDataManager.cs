using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeaponDataManager
{

    static string folderPath => Path.Combine(Application.dataPath, "7.WeaponData");
    static string fileName = "WeaponData.csv";
    string filePath => Path.Combine(folderPath, fileName);

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
        WeaponUI2.Instance.SetPlayerWeaponData(weapons);
    }

    public void CreateDefaultCsv()
    {
        List<WeaponData> defaultWeapons = new List<WeaponData>
        {
            CreateWeaponData("나무검", 1, 1, 10.0f, "woodsword.png", 20, 100, true, true),
            CreateWeaponData("돌검", 1, 5, 15.0f, "stonesword.png", 80, 100, false, false),
            CreateWeaponData("철검", 1, 10, 20.0f, "ironsword.png", 320, 100, false, false),
            CreateWeaponData("황금검", 1, 20, 25.0f, "goldensword.png", 1280, 100, false, false),
            CreateWeaponData("다이아몬드검", 1, 50, 30.0f, "diamondsword.png", 5120, 100, false, false)
        };

        SaveWeapons(defaultWeapons);
    }

    // 무기 데이터 생성
    public WeaponData CreateWeaponData(string weaponName, int upgradeLevel, int weaponDamage, float criticalPercentage, string icon, int purchaseCost, int upgradeCost, bool isPurchased, bool isEquiped)
    {
        WeaponData weapon = ScriptableObject.CreateInstance<WeaponData>();
        weapon.Initialize(weaponName, upgradeLevel, weaponDamage, criticalPercentage, icon, purchaseCost, upgradeCost, isPurchased, isEquiped);
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

            if (data.Length == 9)
            {
                // CSV에서 아이콘 파일명 호출
                string iconFileName = data[4];

                // WeaponData 객체 초기화
                WeaponData weapon = ScriptableObject.CreateInstance<WeaponData>();
                weapon.Initialize(data[0], int.Parse(data[1]), int.Parse(data[2]), float.Parse(data[3]), iconFileName, int.Parse(data[5]), int.Parse(data[6]), bool.Parse(data[7]), bool.Parse(data[8]));

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
            "weaponName,upgradeLevel,weaponDamage,criticalPercentage,icon,purchaseCost,upgradeCost,IsPurchased,IsEquiped"
        };

        foreach (var weapon in weapons)
        {
            lines.Add(weapon.ToCsvString());
        }

        File.WriteAllLines(filePath, lines);
        Debug.Log("무기 데이터가 CSV 파일에 저장되었습니다.");
    }
}
