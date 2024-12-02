using UnityEngine;

public class PlayerCameraMoving : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _startCameraPositionOnPlayer;
    [SerializeField] private float _cameraMovingToPlayerSpeed;

    [SerializeField] private float _defaultMaxCameraPositionY;
    [SerializeField] private float _isTriggerMinCameraPositionY = 4;
    [SerializeField] private float _distanceCameraToPlayerOnForward = 5;

    private PlayerCameraAdditionalTriggera _additionalTrigger;

    private void Awake() => _additionalTrigger = GetComponentInChildren<PlayerCameraAdditionalTriggera>();
    private void Start()
    {
        if (_defaultMaxCameraPositionY == 0)
            _defaultMaxCameraPositionY = transform.position.y;

        transform.position = _startCameraPositionOnPlayer.position;
    }
    private void LateUpdate()
    {
        if (_additionalTrigger.IsTriggerEnter == false)
            CameraMovingToPlayerCameraPosition();
        else
            CameraMovingToPlayerWithPlayerVisibleFalse();
    }

    private void CameraMovingToPlayerCameraPosition()
    {
        Vector3 TargetPosition = new Vector3(_player.position.x, _defaultMaxCameraPositionY, _player.position.z - _distanceCameraToPlayerOnForward);
        transform.position = Vector3.Lerp(transform.position, TargetPosition, _cameraMovingToPlayerSpeed * Time.fixedDeltaTime);
    }
    private void CameraMovingToPlayerWithPlayerVisibleFalse()
    {
        Vector3 TargetPosition = new Vector3(_player.position.x, Mathf.Clamp(_player.position.y, _isTriggerMinCameraPositionY, _player.position.y), _player.position.z - 1);
        transform.position = Vector3.Lerp(transform.position, TargetPosition, _cameraMovingToPlayerSpeed + 0.2f * Time.fixedDeltaTime);
    }
}
