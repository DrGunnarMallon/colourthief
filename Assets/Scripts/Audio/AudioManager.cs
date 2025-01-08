using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public enum AudioType { DrainPaint, MixPaint, Sonar, Tractor, Release, LevelUp }

    private AudioSource musicSource;
    [SerializeField] private AudioClip drainPaintClip = null;
    [SerializeField] private float drainPaintVolume = 0.5f;

    [SerializeField] private AudioClip mixPaintClip = null;
    [SerializeField] private float mixPaintVolume = 0.5f;

    [SerializeField] private AudioClip sonarClip = null;
    [SerializeField] private float sonarVolume = 0.5f;

    [SerializeField] private AudioClip tractorClip = null;
    [SerializeField] private float tractorVolume = 0.5f;

    [SerializeField] private AudioClip levelUpClip = null;
    [SerializeField] private float levelUpVolume = 0.5f;

    [SerializeField] private AudioClip releaseClip = null;
    [SerializeField] private float releaseVolume = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        musicSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioType audioType)
    {
        AudioClip clip = null;
        float volume = 0.5f;

        switch (audioType)
        {
            case AudioType.DrainPaint:
                clip = drainPaintClip;
                volume = drainPaintVolume;
                break;
            case AudioType.MixPaint:
                clip = mixPaintClip;
                volume = mixPaintVolume;
                break;
            case AudioType.Sonar:
                clip = sonarClip;
                volume = sonarVolume;
                break;
            case AudioType.Tractor:
                clip = tractorClip;
                volume = tractorVolume;
                break;
            case AudioType.Release:
                clip = releaseClip;
                volume = releaseVolume;
                break;
            case AudioType.LevelUp:
                clip = levelUpClip;
                volume = levelUpVolume;
                break;
        }

        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.volume = volume;
            musicSource.Play();
        }
    }
}
