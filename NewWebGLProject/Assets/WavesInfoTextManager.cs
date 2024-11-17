using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text), typeof(Animator))]
public class WavesInfoTextManager : MonoBehaviour
{
    [SerializeField] private string _isActiveAnimatorStateName;

    private Animator _animator;

    public Text MainText { get; private set; }

    private void Awake()
    {
        MainText = GetComponent<Text>();
        _animator = GetComponent<Animator>();
    }
    public void TextEnabled() => _animator.SetBool(_isActiveAnimatorStateName, true);
    public void TextDisabled() => _animator.SetBool(_isActiveAnimatorStateName, false);
}
