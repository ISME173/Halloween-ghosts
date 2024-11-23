using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class GameActivatePanel : MonoBehaviour
{
    [SerializeField] private string _isActivePanelAnimatorStateName;
    [SerializeField] private Button _buttonPlay;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _buttonPlay.onClick.AddListener(ButtonClickSoundPlay);
    }
    private void Start() => _animator.SetBool(_isActivePanelAnimatorStateName, true);

    private void ButtonClickSoundPlay() => SoundManager.Instance.PlayAnySound(SoundManager.Instance.AnySoundPlayAudioSource, SoundManager.Instance.ButtonClick);
    public void Disable() => _animator.SetBool(_isActivePanelAnimatorStateName, false);
    public void AddListerToButtonPlayClick(UnityAction unityAction) => _buttonPlay.onClick.AddListener(unityAction);
}
