using UnityEngine;

public class PlayerAnimatorParametersManager : MonoBehaviour
{
    [field: Header("Player animator parameters name"), Space]
    [field: SerializeField] public string Idle { get; private set; }
    [field: SerializeField] public string Died { get; private set; }
}
