using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy states"), Space]

    [SerializeField] protected int _maxHealth;
    [SerializeField] protected Slider _healthSlider;
    [Space]
    [SerializeField] protected EnemyAnyEffect _takeDamageEffect;
    [SerializeField] protected bool _takeDamageEffectIsActive = false;
    [Space]
    [SerializeField] protected EnemyAnyEffect _diedEffect;
    [SerializeField] protected bool _diedEffectIsActive = false;

    protected float _health;
    protected NavMeshAgent _navMeshAgent;
    protected PlayerMoving _playerMoving;
    protected Animator _animator;
    protected Coroutine _healthSliderLookAtToPlayer;
    protected Camera _mainCamera;

    [field: SerializeField] public float AttackDamage { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; } = 1;
    [field: SerializeField] public float IdleTime { get; private set; }
    [field: SerializeField] public float WalkingTime { get; private set; }
    [field: SerializeField] public float AngrySpeed { get; private set; }
    [field: SerializeField] public float MovingSpeed { get; private set; }
    [field: SerializeField] public float DistanceToPlayerForAngry { get; private set; }

    public UnityEvent EnemyDestroy { get; protected set; }
    public float DistanceToPlayer { get; protected set; }
    public Transform Player { get { return _playerMoving.transform; } }

    private IEnumerator HealthSliderLookAtToPlayer()
    {
        while (enabled)
        {
            _healthSlider.transform.parent.LookAt(_mainCamera.transform);
            yield return null;
        }
    }

    protected virtual void InitializedInAwake()
    {
        _health = _maxHealth;
        _healthSlider.maxValue = _health;
        _healthSlider.value = _health;

        _mainCamera = Camera.main;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _playerMoving = FindAnyObjectByType<PlayerMoving>();

        _healthSliderLookAtToPlayer = StartCoroutine(HealthSliderLookAtToPlayer());
    }
    protected virtual void Died()
    {
        if (_diedEffectIsActive)
            Instantiate(_diedEffect, transform.position, Quaternion.identity);

        if (EnemyDestroy != null)
            EnemyDestroy.Invoke();

        Destroy(gameObject);
    }
    public virtual void TakeDamage(int damage)
    {
        if (_takeDamageEffectIsActive)
            Instantiate(_takeDamageEffect, transform.position, Quaternion.identity);

        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
        _healthSlider.value = _health;

        if (_health == 0)
            Died();
    }
}
