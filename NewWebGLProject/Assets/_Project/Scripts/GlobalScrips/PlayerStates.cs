using UnityEngine;
using UnityEngine.Events;

public class PlayerStates : MonoBehaviour
{
    [field: SerializeField] public float AttackDamage { get; private set; }
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float MovingSpeed { get; private set; }
    [field: SerializeField] public float AttackDistance { get; private set; }


    private static PlayerStates _instance;

    private UnityEvent UpgraidPlayerAnyState = new UnityEvent();

    public enum PlayerStatesToUpgraid
    {
        Attack_Damage, Max_Health, Moving_Speed, Attack_Distance
    }

    public static PlayerStates Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<PlayerStates>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<PlayerStates>();
                    singletonObject.name = typeof(PlayerStates).ToString();
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

    public void UpgraidAnyState(PlayerStatesToUpgraid playerStatesToUpgraid, float upgraidValue)
    {
        switch (playerStatesToUpgraid)
        {
            case PlayerStatesToUpgraid.Attack_Damage:
                AttackDamage += upgraidValue;
                break;
            case PlayerStatesToUpgraid.Attack_Distance:
                AttackDistance += upgraidValue;
                break;
            case PlayerStatesToUpgraid.Moving_Speed:
                MovingSpeed += upgraidValue;
                break;
            case PlayerStatesToUpgraid.Max_Health:
                MaxHealth += upgraidValue;
                break;
        }

        if (UpgraidPlayerAnyState != null)
            UpgraidPlayerAnyState.Invoke();
    }
    public void AddListeerToUpgraidPlayerAnyStateUnityEvent(UnityAction unityAction) => UpgraidPlayerAnyState.AddListener(unityAction);
}