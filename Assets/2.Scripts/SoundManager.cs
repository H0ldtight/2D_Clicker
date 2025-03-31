using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioMixer mixer;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip startBgm;
    [SerializeField] private AudioClip mainBgm;
    [SerializeField] private AudioClip defaultSfx; // 효과음
    [SerializeField] private AudioClip startSfx; //시작 효과음
   [SerializeField] private AudioClip mainSfx; //메인 효과음
    [SerializeField] private AudioClip criticalSfx; //크리티컬 효과음

    public void ApplyStartSceneSFX() => ChanageSFX(startSfx); //시작 효과음으로 바로 변경
    public void ApplyMainSceneSFX() => ChanageSFX(mainSfx); // 게임 효과음으로 바로 변경
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        ChangeBGM(startBgm);
    }

    public void SetBGMVolume(float value) //배경음 소리 조절
    {
        if (value <= 0.0001f)
            mixer.SetFloat("BGMVolume", -80f);
        else
            mixer.SetFloat("BGMVolume", Mathf.Log10(value) * 20f);
    }

    public void SetSFXVolume(float value) // 효과음 소리 조절
    {
        if (value <= 0.0001f)
            mixer.SetFloat("SFXVolume", -80f);
        else
            mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20f);
    }
    public void PlaySFX() // 기본 효과음
    {
        if (defaultSfx != null)
            sfxSource.PlayOneShot(defaultSfx);
    }
    public void PlaySFX(AudioClip clip) // 크리티컬 효과음 재생
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void ChanageSFX(AudioClip clip) // 효과음 변경
    {
        if (clip != null)
            defaultSfx = clip;
    }


    public void ChangeBGM(AudioClip clip) // 배경음악 변경
    {
        if (clip == null) return;
        bgmSource.clip = clip;
        bgmSource.Play();
    }

}
