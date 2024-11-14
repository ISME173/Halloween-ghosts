using UnityEngine;
using UnityEngine.AI;

public class GhostAngry : StateMachineBehaviour
{
    private Ghost _ghost;
    private NavMeshAgent _agent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent = animator.GetComponent<NavMeshAgent>();
        _ghost = animator.GetComponent<Ghost>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_ghost.Player.position);

        if (_ghost.DistanceToPlayer > _ghost.DistanceToPlayerForAngry)
            animator.SetBool(_ghost.AngryAnimatorParameterName, false);
    }
}
