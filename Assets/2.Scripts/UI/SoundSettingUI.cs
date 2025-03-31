using UnityEngine;
using UnityEngine.UI;

public class SoundSettingUI : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // 슬라이더 값 초기화
        float bgmValue, sfxValue;
        SoundManager.Instance.mixer.GetFloat("BGMVolume", out bgmValue);
        SoundManager.Instance.mixer.GetFloat("SFXVolume", out sfxValue);

        bgmSlider.value = Mathf.Pow(10, bgmValue / 20f);
        sfxSlider.value = Mathf.Pow(10, sfxValue / 20f);

        // 값이 바뀔 때 볼륨 적용
        bgmSlider.onValueChanged.AddListener(SoundManager.Instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSFXVolume);
    }
}
