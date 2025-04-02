using UnityEngine;
using UnityEngine.EventSystems;

public class BackGroundClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.OnClick();
        SoundManager.Instance.PlaySFX();
    }
}
