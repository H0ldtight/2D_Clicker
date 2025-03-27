using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //플레이어 데이터
    //public Character Player { get; private set; }

    public float finalAttackPower; //최종 데미지
    public float finalCritDamage; // 최종 크리티컬 데미지
    public float finalGoldBonus; // 최종 골드 보너스

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
    }

    public void CalculateFinalStats()
    {

    }

    public void NewPlayerData()
    {
        //플레이어 데이터 생성
    }
    public void LoadPlayerData() //이어 하기시
    {

    }

}
