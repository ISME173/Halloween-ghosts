using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class PlayerWinPanelActivate : MonoBehaviour
{
    [SerializeField] private string _isActiveAnimatorParameter;
    [SerializeField] private Button _restartButton;

    private Animator _animator;

    private void Awake()
    {
        _restartButton.onClick.AddListener(RestartScene);
        _animator = GetComponent<Animator>();
    }

    private void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void ActivatePanel() => _animator.SetBool(_isActiveAnimatorParameter, true);
}
