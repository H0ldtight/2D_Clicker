using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 데이터 보여주는 담당
public class StageUI : MonoBehaviour
{
    [SerializeField] private TMP_Text stageText;
    [SerializeField] private TMP_Text enemyNameText;
    [SerializeField] private TMP_Text enemyCountText;
    

    public void SetStage(int stage)
    {
        stageText.text = $"Stage {stage + 1}";
    }

    public void SetEnemy()
    {
        enemyNameText.text = "";
        enemyCountText.text = "";
    }

    public void UpdateStageText(int stage)
    {
        stageText.text = $"Stage {stage + 1}";
    }
    
    public void UpdateEnemyCount(int count)
    {
        enemyCountText.text = $"{count} / {EnemyManager.Instance.TotalCount}";
    }
}