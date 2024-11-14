using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerCameraAdditionalTriggera : MonoBehaviour
{
    [SerializeField] private float _distanceToPlayerToTriggerObjectSetParent = 4;
    [SerializeField] private float _timeIsOverToReturnCameraInDefaultPosition = 2;
    [SerializeField] private GameObject _triggerObjectPrefab;
    [SerializeField] private Transform _player;

    private Coroutine _returnCameraInDefaultPositionTimer;
    private Coroutine _checkSphereInOnTriggerExit;
    private SphereCollider _sphereCollider;
    private Vector3 _colliderPositionBeforeTriggerExit;
    private GameObject _triggerObject = null;
    private float _distanceToPlayer;

    public bool IsTriggerEnter { get; private set; } = false;
    private void Awake() => _sphereCollider = GetComponent<SphereCollider>();

    private void Update() => _distanceToPlayer = Vector3.Distance(transform.position, _player.position);

    private void OnTriggerStay(Collider other)
    {
        IsTriggerEnter = true;
        _colliderPositionBeforeTriggerExit = transform.position;
    }
    private void OnTriggerExit(Collider other)
    {
        if (_triggerObject == null)
        {
            _triggerObject = Instantiate(_triggerObjectPrefab, _colliderPositionBeforeTriggerExit, Quaternion.identity);
            _checkSphereInOnTriggerExit = StartCoroutine(CheckSphereInOnTriggerExit());
            StartCoroutine(TriggerSetParentTimeIsOver(_triggerObject));
        }
    }
    private IEnumerator TriggerSetParentTimeIsOver(GameObject triggerObject)
    {
        while (triggerObject.transform.parent == null)
        {
            if (_distanceToPlayer <= _distanceToPlayerToTriggerObjectSetParent)
                triggerObject.transform.SetParent(transform);  
            
            yield return null;
        }
    }
    private IEnumerator CheckSphereInOnTriggerExit()
    {
        while (enabled)
        {
            if (Physics.CheckSphere(_triggerObject.transform.position, _sphereCollider.radius) == false)
            {
                _returnCameraInDefaultPositionTimer = StartCoroutine(ReturnCameraInDefaultPositionTimer());

                Destroy(_triggerObject);
                _triggerObject = null;
                StopCoroutine(_checkSphereInOnTriggerExit);
            }

            yield return null;
        }
    }
    private IEnumerator ReturnCameraInDefaultPositionTimer()
    {
        yield return new WaitForSeconds(_timeIsOverToReturnCameraInDefaultPosition);
        IsTriggerEnter = false;
    }
}
