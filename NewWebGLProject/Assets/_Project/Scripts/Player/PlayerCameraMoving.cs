using UnityEngine;

public class PlayerCameraMoving : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _cameraMovingToPlayerSpeed;

    private void Update()
    {
        Vector3 TargetVector = new Vector3(_player.position.x, transform.position.y, _player.position.z - 5);
        transform.position = Vector3.Lerp(transform.position, TargetVector, _cameraMovingToPlayerSpeed * Time.fixedDeltaTime);
    }
}
