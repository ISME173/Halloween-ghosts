using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _attackDamage;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Camera _mainCamera;

    private Animator _animator;
    private PlayerAnimatorParametersManager _playerAnimatorParameters;

    public UnityEvent PlayerDied { get; private set; }
    public UnityEvent PlayerTakeDamage { get; private set; }
    public float Health { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerAnimatorParameters = GetComponent<PlayerAnimatorParametersManager>();
    }
    private void Start()
    {
        Health = _maxHealth;
        _healthSlider.maxValue = Health;
        _healthSlider.value = Health;
    }
    private void Update() => _healthSlider.transform.LookAt(_mainCamera.transform);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            TakeDamage(enemy.AttackDamage);
        }
    }
    private void TakeDamage(float damage)
    {
        if (Health > 0)
        {
            Health -= Mathf.Clamp(damage, 0, _maxHealth);
            _healthSlider.value = Health;

            if (PlayerTakeDamage != null)
                PlayerTakeDamage.Invoke();

            if (Health == 0)
            {
                Died();
            }
        }
    }
    private void Died()
    {
        _animator.SetBool(_playerAnimatorParameters.IsDied, true);

        if (PlayerDied != null)
        {
            PlayerDied.Invoke();
        }

        enabled = false;
    }
}
