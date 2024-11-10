using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _attackDamage;
    [SerializeField] protected int _movingSpeed;

    protected float _health;
    protected NavMeshAgent _navMeshAgent;
    protected PlayerMoving _playerMoving;
    protected Animator _animator;

    protected virtual void InitializedInAwake()
    {
        _health = _maxHealth;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _playerMoving = FindAnyObjectByType<PlayerMoving>();
    }
    protected abstract void Died();
    public virtual void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);

        if (_health <= 0)
            Died();

        Debug.Log("takeDamage");
    }
}
