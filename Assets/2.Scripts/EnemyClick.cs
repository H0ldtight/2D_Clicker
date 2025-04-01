using UnityEngine;

public class EnemyClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("적이 클릭됨!");
        GetComponentInParent<Enemy>().TakeDamage(10);
        GameManager.Instance.OnClick();
        SoundManager.Instance.PlaySFX();
    }
}
