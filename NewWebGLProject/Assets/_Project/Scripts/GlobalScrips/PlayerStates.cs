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
        Attack_damage, Max_health, Moving_speed, Attack_distance
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
        Destroy(Instantiate(PlayerUpgraidsManager.Instance.PlayerUpgraidEffect, transform.position, Quaternion.identity),
            PlayerUpgraidsManager.Instance.TimeToDestroyUpgraidEffect);

        SoundManager.Instance.PlayAnySound(SoundManager.Instance.AnySoundPlayAudioSource, SoundManager.Instance.PlayerUpgraid);

        switch (playerStatesToUpgraid)
        {
            case PlayerStatesToUpgraid.Attack_damage:
                AttackDamage += upgraidValue;
                break;
            case PlayerStatesToUpgraid.Attack_distance:
                AttackDistance += upgraidValue;
                break;
            case PlayerStatesToUpgraid.Moving_speed:
                MovingSpeed += upgraidValue;
                break;
            case PlayerStatesToUpgraid.Max_health:
                MaxHealth += upgraidValue;
                break;
        }

        if (UpgraidPlayerAnyState != null)
            UpgraidPlayerAnyState.Invoke();
    }
    public void AddListenerToUpgraidPlayerAnyStateUnityEvent(UnityAction unityAction) => UpgraidPlayerAnyState.AddListener(unityAction);
}