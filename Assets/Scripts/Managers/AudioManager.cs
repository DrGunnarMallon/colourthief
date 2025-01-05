using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource musicSource;
    [SerializeField] private AudioClip drainPaintClip = null;
    [SerializeField] private float drainPaintVolume = 0.5f;

    [SerializeField] private AudioClip fillPaintClip = null;
    [SerializeField] private float fillPaintVolume = 0.5f;

    [SerializeField] private AudioClip sonarClip = null;
    [SerializeField] private float sonarVolume = 0.5f;

    [SerializeField] private AudioClip tractorClip = null;
    [SerializeField] private float tractorVolume = 0.5f;

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
    }

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void PlayDrainPaintClip()
    {
        if (drainPaintClip != null)
        {
            musicSource.clip = drainPaintClip;
            musicSource.volume = drainPaintVolume;
            musicSource.Play();
        }
    }

    public void PlayFillPaintClip()
    {
        if (fillPaintClip != null)
        {
            musicSource.clip = fillPaintClip;
            musicSource.volume = fillPaintVolume;
            musicSource.Play();
        }
    }

    public void PlaySonarClip()
    {
        if (sonarClip != null)
        {
            musicSource.clip = sonarClip;
            musicSource.volume = sonarVolume;
            musicSource.Play();
        }
    }

    public void PlayTractorClip()
    {
        if (tractorClip != null)
        {
            musicSource.clip = tractorClip;
            musicSource.volume = tractorVolume;
            musicSource.Play();
        }
    }

    public void PlayReleaseClip()
    {
        if (releaseClip != null)
        {
            musicSource.clip = releaseClip;
            musicSource.volume = releaseVolume;
            musicSource.Play();
        }
    }
}
