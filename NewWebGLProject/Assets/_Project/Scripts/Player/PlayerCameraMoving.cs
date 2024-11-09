using System.Collections;
using UnityEngine;

public class PlayerCameraMoving : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _cameraMovingToPlayerSpeed;

    [SerializeField] private float _defaultMaxCameraPositionY;
    [SerializeField] private float _isTriggerMinCameraPositionY = 4f;

    private PlayerCameraAdditionalTriggera _additionalTrigger;

    private void Awake() => _additionalTrigger = GetComponentInChildren<PlayerCameraAdditionalTriggera>();
    private void Start()
    {
        if (_defaultMaxCameraPositionY == 0)
            _defaultMaxCameraPositionY = transform.position.y;
    }
    private void Update()
    {
        if (_additionalTrigger.IsTriggerEnter == false)
            CameraMovingToPlayerCameraPosition();
        else
            CameraMovingToPlayerWithPlayerVisibleFalse();
    }

    private void CameraMovingToPlayerCameraPosition()
    {
        Vector3 TargetPosition = new Vector3(_player.position.x, _defaultMaxCameraPositionY, _player.position.z - 5);
        transform.position = Vector3.Lerp(transform.position, TargetPosition, _cameraMovingToPlayerSpeed * Time.fixedDeltaTime);
    }
    private void CameraMovingToPlayerWithPlayerVisibleFalse()
    {
        Vector3 TargetPosition = new Vector3(_player.position.x, Mathf.Clamp(_player.position.y, _isTriggerMinCameraPositionY, _player.position.y), _player.position.z - 1);
        transform.position = Vector3.Lerp(transform.position, TargetPosition, _cameraMovingToPlayerSpeed * Time.fixedDeltaTime);
    }
}
