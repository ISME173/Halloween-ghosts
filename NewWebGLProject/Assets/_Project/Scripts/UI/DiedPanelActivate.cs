using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class DiedPanelActivate : MonoBehaviour
{
    [SerializeField] private string _isActiveAnimatorParameter;
    [SerializeField] private Button _restartButton;

    private Animator _animator;

    private void Awake()
    {
        _restartButton.onClick.AddListener(RestartScene);
        _animator = GetComponent<Animator>();
    }
    private void RestartScene()
    {
        SoundManager.Instance.PlayAnySound(SoundManager.Instance.AnySoundPlayAudioSource, SoundManager.Instance.ButtonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ActivatePanel()
    {
        _animator.SetBool(_isActiveAnimatorParameter, true);
        SoundManager.Instance.PlayAnySound(SoundManager.Instance.MusicAudioSource, SoundManager.Instance.GameoverMusic);
    }

}
