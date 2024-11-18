using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    [field: SerializeField] public float AttackDamage { get; private set; }
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float MovingSpeed { get; private set; }
    [field: SerializeField] public float AttackDistance { get; private set; }


    private static PlayerStates _instance;

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
}
