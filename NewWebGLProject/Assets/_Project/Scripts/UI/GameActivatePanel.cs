using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameActivatePanel : MonoBehaviour
{
    [SerializeField] private Button _buttonPlay;

    public void AddListerToButtonPlayClick(UnityAction unityAction) => _buttonPlay.onClick.AddListener(unityAction);
}
