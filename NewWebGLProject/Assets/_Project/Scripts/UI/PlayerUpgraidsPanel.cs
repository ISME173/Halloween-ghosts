using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUpgraidsPanel : MonoBehaviour
{
    [SerializeField] private List<UpgraidAnyState> _upgraidButtons = new List<UpgraidAnyState>();

    private UnityEvent<PlayerStates.PlayerStatesToUpgraid, float> UpgraidPlayerState = new UnityEvent<PlayerStates.PlayerStatesToUpgraid, float>();
    private UnityEvent DestroyUpgraidPanel = new UnityEvent();

    private void Awake()
    {
        for (int i = 0; i < _upgraidButtons.Count; i++)
            _upgraidButtons[i].AddListenerToUpgraidButtonClick(UpgraidPlayerStateWithButtonType);
    }

    private void UpgraidPlayerStateWithButtonType(PlayerStates.PlayerStatesToUpgraid playerStatesToUpgraid, float upgraidStrenght)
    {
        PlayerStates.Instance.UpgraidAnyState(playerStatesToUpgraid, upgraidStrenght);
        Destroy(gameObject);
    }

    public void AddListenerToUpgraidPlayerStateUnityEvent(UnityAction<PlayerStates.PlayerStatesToUpgraid, float> unityAction) => UpgraidPlayerState.AddListener(unityAction);
    public void AddListenerToDestroyUpgraidPanelUnityEvent(UnityAction unityAction) => DestroyUpgraidPanel.AddListener(unityAction);

    private void OnDestroy()
    {
        if (DestroyUpgraidPanel != null)
            DestroyUpgraidPanel.Invoke();
    }
}
