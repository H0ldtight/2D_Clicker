using UnityEngine;
using UnityEngine.EventSystems;

public class BackGroundClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("배경이 클릭됨!");
        GameManager.Instance.OnClick();
    }
}
