using UnityEngine;

public class Walk : StateMachineBehaviour
{
    private PlayerAnimatorParametersManager _playerAnimatorParametersManager;
    private PlayerMoving _playerMoving;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerAnimatorParametersManager = animator.GetComponent<PlayerAnimatorParametersManager>();
        _playerMoving = animator.GetComponent<PlayerMoving>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playerMoving.TargetVelosity.x == 0 && _playerMoving.TargetVelosity.z == 0)
        {
            animator.SetBool(_playerAnimatorParametersManager.Idle, true);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
