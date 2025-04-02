using UnityEngine;

public class EnemyClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.OnClick();
    }
}
