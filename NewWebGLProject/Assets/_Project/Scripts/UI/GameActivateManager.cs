using UnityEngine;
using UnityEngine.Events;

public class GameActivateManager : MonoBehaviour
{
    [SerializeField] private GameActivatePanel _gameActivatePanel;

    private UnityEvent GameStart = new UnityEvent();

    private static GameActivateManager _instance;

    public static GameActivateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameActivateManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<GameActivateManager>();
                    singletonObject.name = typeof(GameActivateManager).ToString();
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
    private void Start()
    {
        _gameActivatePanel.AddListerToButtonPlayClick(Play);
        SoundManager.Instance.PlayAnySound(SoundManager.Instance.MusicAudioSource, SoundManager.Instance.MenuMusic);
    }
    private void Play()
    {
        SoundManager.Instance.PlayAnySound(SoundManager.Instance.MusicAudioSource, SoundManager.Instance.GameMusic);

        if (GameStart != null)
            GameStart.Invoke();

        _gameActivatePanel.Disable();
    }
    public void AddListenerToGameStartUnityEvet(UnityAction unityAction) => GameStart.AddListener(unityAction);
}
