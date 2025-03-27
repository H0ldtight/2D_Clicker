using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //플레이어 데이터
    //public Character Player { get; private set; }

    public float gold; //총 골드
    public float gem; // 총 보석

    public float finalAttackPower; //최종 데미지
    public float finalCritDamage; // 최종 크리티컬 데미지
    public float finalGoldBonus = 1; // 최종 골드 보너스

    public bool isPaused; // 일시정지

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
        //finalAttackPower +=
        //finalCritDamage +=
        //finalGoldBonus +=
    }

    public void NewPlayerData()
    {
        //플레이어 데이터 생성
    }
    public void LoadPlayerData() //이어 하기시
    {

    }

    public void OnClick()
    {
        if(!isPaused)
        {
            gold += finalGoldBonus;
            gem += 2f;
            UIManager.Instance.UpdateUI();
        }

    }
}
