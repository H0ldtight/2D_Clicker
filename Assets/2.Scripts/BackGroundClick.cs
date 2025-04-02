using UnityEngine;
using UnityEngine.EventSystems;

public class BackGroundClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.ApplyStartSceneSFX();
        SoundManager.Instance.PlaySFX();
    }
}
