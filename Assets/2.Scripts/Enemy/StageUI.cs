using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageUI : MonoBehaviour
{
    [SerializeField] private TMP_Text stageText;
    [SerializeField] private TMP_Text enemyNameText;
    [SerializeField] private TMP_Text enemyCountText;
    

    public void SetStage(int stage)
    {
        stageText.text = $"Stage {stage + 1}";
    }

    public void SetEnemy(string name, int count)
    {
        enemyNameText.text = name;
        enemyCountText.text = $"x {count}";
    }

    public void UpdateEnemyCount(int count)
    {
        enemyCountText.text = $"x {count}";
    }
}