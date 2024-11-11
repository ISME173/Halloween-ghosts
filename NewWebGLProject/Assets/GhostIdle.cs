using UnityEngine;
using UnityEngine.AI;

public class GhostIdle : StateMachineBehaviour
{
    private Ghost _ghost;
    private NavMeshAgent _agent;
    private float _time;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _ghost = animator.GetComponent<Ghost>();
        _agent = animator.GetComponent<NavMeshAgent>();

        _agent.SetDestination(animator.transform.position);
        _time = 0;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_ghost.DistanceToPlayer < _ghost.DistanceToPlayerForAngry)
            animator.SetBool(_ghost.AngryAnimatorParameterName, true);

        _time += Time.deltaTime;
        if (_time > _ghost.IdleTime)
            animator.SetBool(_ghost.IdleAnimatorParameterName, false);
    }
}
