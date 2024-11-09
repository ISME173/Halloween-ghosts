using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerCameraAdditionalTriggera : MonoBehaviour
{
    [SerializeField] private float _timeIsOverToReturnCameraInDefaultPosition = 2;
    [SerializeField] private GameObject _triggerObjectPrefab;

    private Coroutine _returnCameraInDefaultPositionTimer;
    private Coroutine _checkSphereInOntriggerExit;
    private Vector3 _colliderPositionBeforeTriggerExit;
    private GameObject _triggerObject = null;

    public bool IsTriggerEnter { get; private set; } = false;

    private void OnTriggerExit(Collider other)
    {
        _triggerObject = Instantiate(_triggerObjectPrefab, _colliderPositionBeforeTriggerExit, Quaternion.identity);

        _checkSphereInOntriggerExit = StartCoroutine(CheckSphereInOnTriggerExit());
        StartCoroutine(TriggerSetParentTimeIsOver(_triggerObject));
    }
    private void OnTriggerStay(Collider other)
    {
        IsTriggerEnter = true;
        _colliderPositionBeforeTriggerExit = transform.position;
    }
    private IEnumerator TriggerSetParentTimeIsOver(GameObject triggerObject)
    {
        yield return new WaitForSeconds(_timeIsOverToReturnCameraInDefaultPosition);
        triggerObject.transform.SetParent(transform.parent);
    }
    private IEnumerator CheckSphereInOnTriggerExit()
    {
        while (enabled)
        {
            if (Physics.CheckSphere(_triggerObject.transform.position, 0.66f) == false)
            {
                _returnCameraInDefaultPositionTimer = StartCoroutine(ReturnCameraInDefaultPositionTimer());

                Destroy(_triggerObject);
                StopCoroutine(_checkSphereInOntriggerExit);
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
