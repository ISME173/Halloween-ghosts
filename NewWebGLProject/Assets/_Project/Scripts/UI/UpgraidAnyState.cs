using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgraidAnyState : MonoBehaviour
{
    [SerializeField] private PlayerStates.PlayerStatesToUpgraid _upgraidType;
    [SerializeField] private float _upgraidStrength;

    private Button _upgraidButton;

    private UnityEvent<PlayerStates.PlayerStatesToUpgraid, float> UpgraidButtonClick = new UnityEvent<PlayerStates.PlayerStatesToUpgraid, float>();

    private void Awake()
    {
        _upgraidButton = GetComponent<Button>();
        _upgraidButton.onClick.AddListener(UpgraidButtonClickUnityEventInvoke);
    }
    private void UpgraidButtonClickUnityEventInvoke()
    {
        if (UpgraidButtonClick != null)
            UpgraidButtonClick.Invoke(_upgraidType, _upgraidStrength);
    }
    public void AddListenerToUpgraidButtonClick(UnityAction<PlayerStates.PlayerStatesToUpgraid, float> unityAction) => UpgraidButtonClick.AddListener(unityAction);
}
