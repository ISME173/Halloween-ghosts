using UnityEngine;

public class PlayerAnimatorParametersManager : MonoBehaviour
{
    [field: Header("Player animator parameters name"), Space]
    [field: SerializeField] public string IsIdle { get; private set; }
    [field: SerializeField] public string IsDied { get; private set; }
}
