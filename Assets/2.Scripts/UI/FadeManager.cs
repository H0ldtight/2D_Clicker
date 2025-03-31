using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [SerializeField] private Image fadeImage;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>씬 시작 시 페이드 아웃</summary>
    public void FadeIn()
    {
        fadeImage.DOKill(); // 기존 tween 제거
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 1); // 완전 검정
        // 2초 동안 천천히 투명하게
        fadeImage.DOFade(0, 2f) // 투명 , 2초 동안
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
        {
            fadeImage.gameObject.SetActive(false);
        });
    }
}