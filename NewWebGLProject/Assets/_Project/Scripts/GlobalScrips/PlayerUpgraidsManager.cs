using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUpgraidsManager : MonoBehaviour
{
    [SerializeField] private List<PlayerUpgraidsPanel> _playerUpgraidsPanelsPrefabs = new List<PlayerUpgraidsPanel>();
    [SerializeField] private Transform _positionUpgraidPanelsPrefabs;
    [SerializeField] private Canvas _mainCanvas;

    private UnityEvent DestroyUpgraidPanel = new UnityEvent();

    private static PlayerUpgraidsManager _instance;

    public static PlayerUpgraidsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<PlayerUpgraidsManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<PlayerUpgraidsManager>();
                    singletonObject.name = typeof(PlayerUpgraidsManager).ToString();
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

    private void Start() => WavesManager.Instance.AddListenerToWaveEndUnityEvent(OpenUpgraidPanel);

    private void OpenUpgraidPanel()
    {
        PlayerUpgraidsPanel playerUpgraidsPanel = Instantiate(_playerUpgraidsPanelsPrefabs[Random.Range(0, _playerUpgraidsPanelsPrefabs.Count)], _positionUpgraidPanelsPrefabs.position, Quaternion.identity);
        playerUpgraidsPanel.transform.SetParent(_mainCanvas.transform);
        playerUpgraidsPanel.transform.position = _positionUpgraidPanelsPrefabs.position;
        playerUpgraidsPanel.AddListenerToDestroyUpgraidPanelUnityEvent(DestroyUpgraidPanelEventInvoke);
    }
    private void DestroyUpgraidPanelEventInvoke()
    {
        if (DestroyUpgraidPanel != null)
            DestroyUpgraidPanel.Invoke();
    }
    public void AddListenerToDestroyUpgraidPanelEvent(UnityAction unityAction) => DestroyUpgraidPanel.AddListener(unityAction);
}