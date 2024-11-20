using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiedPanelActivate : MonoBehaviour
{
    [SerializeField] private string _isActiveAnimatorParameter;
    [SerializeField] private Animator _animator;
    [SerializeField] private Button _restartButton;

    private void Awake() => _restartButton.onClick.AddListener(RestartScene);
    private void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void ActivatePanel() => _animator.SetBool(_isActiveAnimatorParameter, true);
    
}
