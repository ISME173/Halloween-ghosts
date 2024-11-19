using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private WavesInfoTextManager _wavesInfoTextManager;
    [SerializeField] private float _wavesTextAppearanceAnimTime;
    [Space]
    [SerializeField] private string _waveEndText;
    [SerializeField] private string _waveStartText;

    private static WavesManager _instance;

    private int _waveNumber = 1;

    private UnityEvent<int> WaveStartEvent = new UnityEvent<int>();
    private UnityEvent WaveEndEvent = new UnityEvent();

    public static WavesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<WavesManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<WavesManager>();
                    singletonObject.name = typeof(WavesManager).ToString();
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
        EnemySpawner.Instance.AddListenerToAllEnemyDestroyedUnityEvent(WaveEnd);
        PlayerUpgraidsManager.Instance.AddListenerToDestroyUpgraidPanelEvent(WaveStart);

        WaveStart();
    }
    private void WaveStart()
    {
        if (EnemySpawner.Instance.WavesCount < _waveNumber)
        {
            Debug.Log("Waves end");
            return;
        }

        if (WaveStartEvent != null)
            WaveStartEvent.Invoke(_waveNumber);

        _wavesInfoTextManager.MainText.text = _waveStartText;
        StartCoroutine(TextWaveInfoEnabled());
    }
    private void WaveEnd()
    {
        if (WaveEndEvent != null)
            WaveEndEvent.Invoke();

        _waveNumber++;

        _wavesInfoTextManager.MainText.text = _waveEndText;
        StartCoroutine(TextWaveInfoEnabled());
    }
    private IEnumerator TextWaveInfoEnabled()
    {
        _wavesInfoTextManager.TextEnabled();
        yield return new WaitForSeconds(_wavesTextAppearanceAnimTime);
        _wavesInfoTextManager.TextDisabled();
    }

    public void AddListenerToWaveStartUnityEvent(UnityAction<int> unityAction) => WaveStartEvent.AddListener(unityAction);
    public void AddListenerToWaveEndUnityEvent(UnityAction unityAction) => WaveEndEvent.AddListener(unityAction);
}
