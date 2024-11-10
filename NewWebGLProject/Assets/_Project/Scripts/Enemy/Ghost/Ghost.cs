using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class Ghost : Enemy
{
    [Space]
    [SerializeField] private string _diedAnimatorParameterName;

    private void Awake()
    {
        InitializedInAwake();
    }
    protected override void Died()
    {
        _animator.SetBool(_diedAnimatorParameterName, true);
        Destroy(gameObject);
    }
}
