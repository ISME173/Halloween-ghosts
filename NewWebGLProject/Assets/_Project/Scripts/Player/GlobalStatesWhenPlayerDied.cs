using System.Collections.Generic;
using UnityEngine;

public class GlobalStatesWhenPlayerDied : MonoBehaviour
{
    private List<Behaviour> _behavioursToSetEnebledFalse = new List<Behaviour>();
    private PlayerLifeManager _playerLifeManager;

    private static GlobalStatesWhenPlayerDied _instance;

    public static GlobalStatesWhenPlayerDied Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GlobalStatesWhenPlayerDied>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<GlobalStatesWhenPlayerDied>();
                    singletonObject.name = typeof(GlobalStatesWhenPlayerDied).ToString();
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        _playerLifeManager = FindAnyObjectByType<PlayerLifeManager>();
    }
    private void Start() => _playerLifeManager.AddListenerToPlayerDiedUnnityEvent(BehavioursInListSetEneblesFalse);

    private void BehavioursInListSetEneblesFalse()
    {
        for (int i = 0; i < _behavioursToSetEnebledFalse.Count; i++)
            _behavioursToSetEnebledFalse[i].enabled = false;
    }
    public void AddBehaviourInListToSetEnebledFalseWhenPlayerDied(Behaviour behaviour)
    {
        _behavioursToSetEnebledFalse.Add(behaviour);
    }
}
