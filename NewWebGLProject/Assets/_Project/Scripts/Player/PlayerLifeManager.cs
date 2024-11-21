using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Camera _mainCamera;

    private float _maxHealth;
    private Animator _animator;
    private PlayerAnimatorParametersManager _playerAnimatorParameters;
    private Rigidbody _rigidbody;
    private Collider _collider;

    private UnityEvent PlayerDied = new UnityEvent();

    public UnityEvent PlayerTakeDamage { get; private set; }
    public float Health { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerAnimatorParameters = GetComponent<PlayerAnimatorParametersManager>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        PlayerStates.Instance.AddListeerToUpgraidPlayerAnyStateUnityEvent(UpdateMaxHealthValue);
    }
    private void Start()
    {
        _maxHealth = PlayerStates.Instance.MaxHealth;

        Health = _maxHealth;
        _healthSlider.maxValue = Health;
        _healthSlider.value = Health;
    }
    private void Update() => _healthSlider.transform.LookAt(_mainCamera.transform);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
            TakeDamage(enemy.AttackDamage);
    }
    private void UpdateMaxHealthValue()
    {
        _maxHealth = PlayerStates.Instance.MaxHealth;
        Health = _maxHealth;
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _maxHealth;
    }
    private void TakeDamage(float damage)
    {
        if (Health > 0)
        {
            Health -= Mathf.Clamp(damage, 0, _maxHealth);
            _healthSlider.value = Health;

            if (PlayerTakeDamage != null)
                PlayerTakeDamage.Invoke();

            if (Health <= 0)
            {
                Died();
            }
        }
    }
    private void Died()
    {
        _collider.enabled = false;
        _rigidbody.isKinematic = true;

        _animator.SetBool(_playerAnimatorParameters.IsDied, true);

        if (PlayerDied != null)
            PlayerDied.Invoke();

        enabled = false;
    }
    public void AddListenerToPlayerDiedUnityEvent(UnityAction unityAction) => PlayerDied.AddListener(unityAction);
}
