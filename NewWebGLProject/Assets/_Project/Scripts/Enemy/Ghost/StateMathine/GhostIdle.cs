using UnityEngine;
using UnityEngine.AI;

public class GhostIdle : StateMachineBehaviour
{
    private Ghost _ghost;
    private NavMeshAgent _agent;
    private float _time;
    private float _idleTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _ghost = animator.GetComponent<Ghost>();
        _agent = animator.GetComponent<NavMeshAgent>();

        _time = 0;
        _idleTime = Random.Range(1, Mathf.Clamp(_ghost.IdleTime, 2, _ghost.IdleTime));

        _agent.SetDestination(animator.transform.position);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_ghost.DistanceToPlayer < _ghost.DistanceToPlayerForAngry)
            animator.SetBool(_ghost.AngryAnimatorParameterName, true);

        _time += Time.deltaTime;
        if (_time > _idleTime)
            animator.SetBool(_ghost.IdleAnimatorParameterName, false);
    }
}
