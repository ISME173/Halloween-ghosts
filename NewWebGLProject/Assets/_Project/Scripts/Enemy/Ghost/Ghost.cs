using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class Ghost : Enemy
{
    [field: Header("Animator states"), Space]
    [field: SerializeField] public string DiedAnimatorParameterName { get; private set; }
    [field: SerializeField] public string IdleAnimatorParameterName { get; private set; }
    [field: SerializeField] public string AngryAnimatorParameterName { get; private set; }
 
    private void Awake() => InitializedInAwake();
    private void Update()
    {
        DistanceToPlayer = Vector3.Distance(transform.position, _playerMoving.transform.position);
        _healthSlider.transform.LookAt(_mainCamera.transform);
    }
    protected override void Died()
    {
        _animator.SetBool(DiedAnimatorParameterName, true);
        base.Died();
    }
}
