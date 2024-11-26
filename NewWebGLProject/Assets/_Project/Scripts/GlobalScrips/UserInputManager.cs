using UnityEngine;

public class UserInputManager : MonoBehaviour
{
    [field: SerializeField] public Camera MainCamera { get; private set; }
    [field: SerializeField] public Canvas MainCanvas { get; private set; }

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

    // можно дописать нужный функционал
}
