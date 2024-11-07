using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoving : MonoBehaviour
{
    [SerializeField] private DynamicJoystick _playerJoystick;
    [SerializeField] private float _speed;

    private Rigidbody _playerRigidbody;

    public Vector3 TargetVelosity { get; private set; }

    private void Awake() => _playerRigidbody = GetComponent<Rigidbody>();
    private void FixedUpdate()
    {
        _playerRigidbody.velocity = new Vector3(_playerJoystick.Horizontal * _speed, _playerRigidbody.velocity.y, _playerJoystick.Vertical * _speed);
        TargetVelosity = _playerRigidbody.velocity;

        if (_playerJoystick.Horizontal != 0 || _playerJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(TargetVelosity.x, 0, TargetVelosity.z));
        }
    }
}
