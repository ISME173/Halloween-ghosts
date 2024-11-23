using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [field: Header("Sounds"), Space]
    [field: SerializeField] public AudioClip ButtonClick { get; private set; }
    [field: SerializeField] public AudioClip EnemyDied { get; private set; }
    [field: SerializeField] public AudioClip PlayerGunShooting { get; private set; }

    [field: Header("Musics"), Space]
    [field: SerializeField] public AudioClip MenuMusic { get; private set; }
    [field: SerializeField] public AudioClip GameoverMusic { get; private set; }
    [field: SerializeField] public AudioClip GameMusic { get; private set; }

    [field: Header("Audio sources"), Space]
    [field: SerializeField] public AudioSource MusicAudioSource { get; private set; }
    [field: SerializeField] public AudioSource AnySoundPlayAudioSource { get; private set; }

    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<SoundManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<SoundManager>();
                    singletonObject.name = typeof(SoundManager).ToString();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }
    public void PlayAnySound(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioSource != null && audioClip != null)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            if (audioSource == MusicAudioSource)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            else
            {
                audioSource.PlayOneShot(audioClip);
            }
        }
    }
}