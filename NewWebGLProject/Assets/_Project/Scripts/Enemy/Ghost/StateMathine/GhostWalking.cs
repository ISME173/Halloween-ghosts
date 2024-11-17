using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostWalking : StateMachineBehaviour
{
    private List<Transform> _walkingPoints = new List<Transform>();
    private Ghost _ghost;
    private NavMeshAgent _agent;
    private float _time;
    private float _walkingTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _ghost = animator.GetComponent<Ghost>();
        _agent = animator.GetComponent<NavMeshAgent>();
        _walkingPoints = EnemyWalkingPoints.Instance.GetEnemyWalkingPoints();

        _time = 0;
        _agent.speed = _ghost.MovingSpeed;
        _walkingTime = Random.Range(1, Mathf.Clamp(_ghost.WalkingTime, 2, _ghost.WalkingTime));
;
        _agent.SetDestination(_walkingPoints[Random.Range(0, _walkingPoints.Count)].position);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_agent.remainingDistance < _agent.stoppingDistance)
            _agent.SetDestination(_walkingPoints[Random.Range(0, _walkingPoints.Count)].position);

        if (_ghost.DistanceToPlayer < _ghost.DistanceToPlayerForAngry)
            animator.SetBool(_ghost.AngryAnimatorParameterName, true);

        _time += Time.deltaTime;
        if (_time > _walkingTime)
            animator.SetBool(_ghost.IdleAnimatorParameterName, true);
    }
}
