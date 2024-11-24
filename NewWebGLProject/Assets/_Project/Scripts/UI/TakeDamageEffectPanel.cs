using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TakeDamageEffectPanel : MonoBehaviour
{
    [SerializeField] private string _isActiveAnimatorStateName;

    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();
    public void ActivateEffect() => _animator.SetBool(_isActiveAnimatorStateName, true);
    public void DisableEffect() => _animator.SetBool(_isActiveAnimatorStateName, false);
}
