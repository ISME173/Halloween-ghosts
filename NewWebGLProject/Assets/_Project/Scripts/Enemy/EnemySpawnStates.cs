using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnStates : MonoBehaviour
{
    [field: Header("If needed types in childrens != null, empty"), Space]
    [field: SerializeField] public List<EnemyForSpawn> EnemyForSpawnList { get; private set; } = new List<EnemyForSpawn>();

    private void Awake()
    {
        if (EnemyForSpawnList.Count == 0)
        {
            EnemyForSpawn[] enemyForSpawn = GetComponentsInChildren<EnemyForSpawn>();
            for (int i = 0; i < enemyForSpawn.Length; i++)
                EnemyForSpawnList.Add(enemyForSpawn[i]);
        }
    }
}
