using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private Button _fullScreenButton;

    private static PlayerInputManager _instance;

    public static PlayerInputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerInputManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<PlayerInputManager>();
                    singletonObject.name = typeof(PlayerInputManager).ToString();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddListanerToFullScreenButton(UnityAction action)
    {
        _fullScreenButton.onClick.AddListener(action);
    }
}
