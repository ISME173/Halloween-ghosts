using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy states"), Space]

    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _attackDamage;

    protected float _health;
    protected NavMeshAgent _navMeshAgent;
    protected PlayerMoving _playerMoving;
    protected Animator _animator;

    [field: SerializeField] public float IdleTime { get; private set; }
    [field: SerializeField] public float WalkingTime { get; private set; }
    [field: SerializeField] public float AngrySpeed { get; private set; }
    [field: SerializeField] public float DistanceToPlayerForAngry { get; private set; }
    [field: SerializeField] public float MovingSpeed { get; protected set; }

    public UnityEvent EnemyDestroy { get; protected set; }
    public float DistanceToPlayer { get; protected set; }

    protected virtual void InitializedInAwake()
    {
        _health = _maxHealth;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _playerMoving = FindAnyObjectByType<PlayerMoving>();
    }
    protected virtual void Died()
    {
        if (EnemyDestroy != null)
            EnemyDestroy.Invoke();
    }
    public virtual void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);

        if (_health <= 0)
            Died();
    }
}
