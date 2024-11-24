using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Text), typeof(Animator))]
public class WavesInfoTextManager : MonoBehaviour
{
    [SerializeField] private string _isActiveAnimatorStateName;

    private Animator _animator;
    private Text _mainText;

    private void Awake()
    {
        _mainText = GetComponent<Text>();
        _animator = GetComponent<Animator>();

        WavesManager.Instance.AddListenerToWaveStartUnityEvent(ChangeTextWhenWaveStart);
        WavesManager.Instance.AddListenerToWaveEndUnityEvent(ChangeTextWhenWaveEnd);
    }

    private async void ChangeTextWhenWaveStart(int anyState)
    {
        while (!YandexGame.SDKEnabled)
        {
            await Task.Delay(200);
        }

        string currentText;
        switch (YandexGame.EnvironmentData.language)
        {
            case "ru":
                currentText = $"{WavesManager.Instance.WavesNumber} волна началась!";
                break;
            case "en":
                currentText = $"Wave {WavesManager.Instance.WavesNumber} start!";
                break;
            case "tr":
                currentText = $"{WavesManager.Instance.WavesNumber} dalga başladı!";
                break;
            default:
                currentText = $"Wave {WavesManager.Instance.WavesNumber} start!";
                break;
        }
        _mainText.text = currentText;
    }
    private async void ChangeTextWhenWaveEnd()
    {
        while (!YandexGame.SDKEnabled)
        {
            await Task.Delay(200);
        }

        string currentText;
        switch (YandexGame.EnvironmentData.language)
        {
            case "ru":
                currentText = $"Волна закончилась!";
                break;
            case "en":
                currentText = $"Wave end!";
                break;
            case "tr":
                currentText = $"Dalga sona erdi!";
                break;
            default:
                currentText = $"Wave {WavesManager.Instance.WavesNumber} end!";
                break;
        }
        _mainText.text = currentText;
    }

    public void TextEnabled() => _animator.SetBool(_isActiveAnimatorStateName, true);
    public void TextDisabled() => _animator.SetBool(_isActiveAnimatorStateName, false);
}
