using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy states"), Space]

    [SerializeField] protected int _maxHealth;
    [SerializeField] protected Slider HealthSlider;
    [Space]
    [SerializeField] protected EnemyAnyEffect _takeDamageEffect;
    [SerializeField] protected bool _takeDamageEffectIsActive = false;
    [Space]
    [SerializeField] protected EnemyAnyEffect _diedEffect;
    [SerializeField] protected bool _diedEffectIsActive = false;
    [Space]

    protected float Health;
    protected NavMeshAgent NavMeshAgent;
    protected PlayerMoving PlayerMoving;
    protected Animator Animator;
    protected Coroutine HealthSliderLookAtToPlayer;
    protected Camera MainCamera;

    [field: SerializeField] public float AttackDamage { get; private set; }
    [field: SerializeField] public float IdleTime { get; private set; }
    [field: SerializeField] public float WalkingTime { get; private set; }
    [field: SerializeField] public float AngrySpeed { get; private set; }
    [field: SerializeField] public float MovingSpeed { get; private set; }
    [field: SerializeField] public float DistanceToPlayerForAngry { get; private set; }

    public UnityEvent<Enemy> EnemyDestroy;
    public float DistanceToPlayer { get; protected set; }
    public Transform Player { get; private set; }

    protected virtual void InitializedInAwake()
    {
        Health = _maxHealth;
        HealthSlider.maxValue = Health;
        HealthSlider.value = Health;

        MainCamera = UserInputManager.Instance.MainCamera;

        NavMeshAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        PlayerMoving = FindAnyObjectByType<PlayerMoving>();

        Player = PlayerMoving.transform;

        EnemyDestroy.AddListener(EnemySpawner.Instance.CheckCountEnabledEnemy);

        if (transform.parent != null)
            transform.SetParent(null);
    }
    protected virtual void Died()
    {
        SoundManager.Instance.PlayAnySound(SoundManager.Instance.AnySoundPlayAudioSource, SoundManager.Instance.EnemyDied);

        if (_diedEffectIsActive && _diedEffect != null)
            Instantiate(_diedEffect, transform.position, Quaternion.identity);

        if (EnemyDestroy != null)
            EnemyDestroy.Invoke(this);

        Destroy(gameObject);
    }
    public virtual void TakeDamage(int damage)
    {
        if (_takeDamageEffectIsActive)
            Instantiate(_takeDamageEffect, transform.position, Quaternion.identity);

        Health = Mathf.Clamp(Health - damage, 0, _maxHealth);
        HealthSlider.value = Health;

        if (Health == 0)
            Died();
    }
}
