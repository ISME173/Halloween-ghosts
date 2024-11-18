using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UserInputManager : MonoBehaviour
{
    [SerializeField] private Button _fullScreenButton;

    private static UserInputManager _instance;

    public static UserInputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<UserInputManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<UserInputManager>();
                    singletonObject.name = typeof(UserInputManager).ToString();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    public void AddListanerToFullScreenButtonOnClick(UnityAction action)
    {
        _fullScreenButton.onClick.AddListener(action);
    }
}
