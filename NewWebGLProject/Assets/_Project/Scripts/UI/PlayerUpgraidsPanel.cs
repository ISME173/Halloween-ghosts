using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YG;

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
    private void OnDestroy()
    {
        if (DestroyUpgraidPanel != null)
            DestroyUpgraidPanel.Invoke();
    }

    private void UpgraidPlayerStateWithButtonType(PlayerStates.PlayerStatesToUpgraid playerStatesToUpgraid, float upgraidStrenght)
    {
        SoundManager.Instance.PlayAnySound(SoundManager.Instance.AnySoundPlayAudioSource, SoundManager.Instance.ButtonClick);
        PlayerStates.Instance.UpgraidAnyState(playerStatesToUpgraid, upgraidStrenght);
        gameObject.SetActive(false);
        Destroy(gameObject, 0.3f);

        YandexGame.FullscreenShow();
    }

    public void AddListenerToUpgraidPlayerStateUnityEvent(UnityAction<PlayerStates.PlayerStatesToUpgraid, float> unityAction) => UpgraidPlayerState.AddListener(unityAction);
    public void AddListenerToDestroyUpgraidPanelUnityEvent(UnityAction unityAction) => DestroyUpgraidPanel.AddListener(unityAction);
}
