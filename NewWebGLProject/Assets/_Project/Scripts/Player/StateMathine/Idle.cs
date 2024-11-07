using UnityEngine;

public class Idle : StateMachineBehaviour
{
    private PlayerAnimatorParametersManager _animatorParametersManager;
    private PlayerMoving _playerMoving;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _animatorParametersManager = animator.GetComponent<PlayerAnimatorParametersManager>();
        _playerMoving = animator.GetComponent<PlayerMoving>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playerMoving.TargetVelosity.x != 0 || _playerMoving.TargetVelosity.z != 0)
        {
            animator.SetBool(_animatorParametersManager.Idle, false);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
