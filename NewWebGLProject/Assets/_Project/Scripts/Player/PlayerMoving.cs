using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoving : MonoBehaviour
{
    [SerializeField] private Joystick _playerJoystick;

    private float _speed;
    private Rigidbody _playerRigidbody;
    private PlayerAttackZone _playerAttackZone;
    private bool _isMoving = true;

    public Vector3 TargetVelosity { get; private set; }

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAttackZone = FindAnyObjectByType<PlayerAttackZone>();

        PlayerStates.Instance.AddListeerToUpgraidPlayerAnyStateUnityEvent(UpdateMovingStates);
        PlayerUpgraidsManager.Instance.AddListenerToDestroyUpgraidPanelEvent(IsMovingTrue);
        WavesManager.Instance.AddListenerToWaveEndUnityEvent(IsMovingFalse);
    }
    private void Start()
    {
        GlobalStatesWhenPlayerDied.Instance.AddBehaviourInListToSetEnebledFalseWhenPlayerDied(this);
        GlobalStatesWhenPlayerDied.Instance.AddGameObjectInListToSetActiveFalseWhenPlayerDied(_playerJoystick.gameObject);
        _speed = PlayerStates.Instance.MovingSpeed;
    }
    private void FixedUpdate()
    {
        if (_isMoving == false)
            return;

        _playerRigidbody.velocity = new Vector3(_playerJoystick.Horizontal * _speed, _playerRigidbody.velocity.y, _playerJoystick.Vertical * _speed);
        TargetVelosity = _playerRigidbody.velocity;

        if ((_playerJoystick.Horizontal != 0 || _playerJoystick.Vertical != 0) && _playerAttackZone.ClosestEnemy == null)
            transform.rotation = Quaternion.LookRotation(new Vector3(TargetVelosity.x, 0, TargetVelosity.z));

        else if (_playerAttackZone.ClosestEnemy != null)
            transform.LookAt(_playerAttackZone.ClosestEnemy.transform.position);
    }
    private void UpdateMovingStates() => _speed = PlayerStates.Instance.MovingSpeed;
    private void IsMovingTrue() => _isMoving = true;
    private void IsMovingFalse() => _isMoving = false;
}
