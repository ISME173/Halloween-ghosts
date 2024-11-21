using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalStatesIfPlayerWin : MonoBehaviour
{
    [SerializeField] private PlayerWinPanelActivate _playerWinPanelActivate; 

    private List<GameObject> _gameObjectsToSetActiveFalse = new List<GameObject>();
    private List<Behaviour> _behavioursToSetEnables = new List<Behaviour>();

    private static GlobalStatesIfPlayerWin _instance;

    private UnityEvent PlayerWin = new UnityEvent();

    public static GlobalStatesIfPlayerWin Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GlobalStatesIfPlayerWin>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<GlobalStatesIfPlayerWin>();
                    singletonObject.name = typeof(GlobalStatesIfPlayerWin).ToString();
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

        WavesManager.Instance.AddListenerToAllWavesEndUnityEvent(PlayerWinUnityEventInvoke);
    }
    private void PlayerWinUnityEventInvoke()
    {
        if (PlayerWin != null)
            PlayerWin.Invoke();

        _playerWinPanelActivate.ActivatePanel();

        for (int i = 0; i < _behavioursToSetEnables.Count; i++)
            _behavioursToSetEnables[i].enabled = false;

        for (int i = 0; i < _gameObjectsToSetActiveFalse.Count; i++)
            _gameObjectsToSetActiveFalse[i].SetActive(false);
    }

    public void AddBehaviourInListToSetEnebledFalseWhenPlayerWin(Behaviour behaviour) => _behavioursToSetEnables.Add(behaviour);
    public void AddGameObjectInListToSetActiveFalseWhenPlayerWin(GameObject gameObject) => _gameObjectsToSetActiveFalse.Add(gameObject);
    public void AddListenerToPlayerWinUnityEvent(UnityAction unityAction) => PlayerWin.AddListener(unityAction);
}