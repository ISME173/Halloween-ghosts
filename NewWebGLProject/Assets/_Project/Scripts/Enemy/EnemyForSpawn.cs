using UnityEngine;

public class EnemyForSpawn : MonoBehaviour
{
    [field: SerializeField] public Enemy Enemy {  get; private set; }
    [field: SerializeField] public int CountEnemyForSpawn { get; private set; }
}
