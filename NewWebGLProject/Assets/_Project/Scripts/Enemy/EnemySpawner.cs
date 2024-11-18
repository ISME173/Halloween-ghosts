using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("If needed types in childrens != null, empty"), Space]
    [SerializeField] private List<EnemySpawnStates> _enemySpawnStates = new List<EnemySpawnStates>();

    private List<Transform> _pointsToSpawnEnemy;
    private List<Enemy> _enemyInScene = new List<Enemy>();

    public static EnemySpawner Instance;

    private UnityEvent EnemySpawnStart = new UnityEvent();
    private UnityEvent AllEnemyInSceneDestroyed = new UnityEvent();

    public int WavesCount { get; private set; }

    private void Awake()
    {
        Instance = this;

        _pointsToSpawnEnemy = EnemyWalkingPoints.Instance.GetEnemyWalkingPoints();

        if (_enemySpawnStates.Count == 0)
        {
            EnemySpawnStates[] enemySpawnStates = GetComponentsInChildren<EnemySpawnStates>();
            for (int i = 0; i < enemySpawnStates.Length; i++)
                _enemySpawnStates.Add(enemySpawnStates[i]);
        }
        WavesCount = _enemySpawnStates.Count;

        WavesManager.Instance.AddListenerToWaveStartUnityEvent(SpawnEnemy);
    }

    private void SpawnEnemy(int waveNumber)
    {
        if (EnemySpawnStart != null)
            EnemySpawnStart.Invoke();

        List<EnemyForSpawn> enemyForSpawnArray = _enemySpawnStates[waveNumber - 1].EnemyForSpawnList;

        for (int i = 0; i < enemyForSpawnArray.Count; i++)
        {
            for (int j = 0; j < enemyForSpawnArray[i].CountEnemyForSpawn; j++)
            {
                Enemy enemy = Instantiate(enemyForSpawnArray[i].Enemy, _pointsToSpawnEnemy[UnityEngine.Random.Range(0, _pointsToSpawnEnemy.Count)]
                    .position, Quaternion.identity);

                _enemyInScene.Add(enemy);
            }
        }
    }
    public void CheckCountEnabledEnemy(Enemy removeEnemyInList = null)
    {
        if (removeEnemyInList != null)
            _enemyInScene.Remove(removeEnemyInList);

        for (int i = 0; i < _enemyInScene.Count; i++)
        {
            if (_enemyInScene[i].gameObject == null)
                _enemyInScene.RemoveAt(i);
        }

        if (_enemyInScene.Count == 0)
        {
            if (AllEnemyInSceneDestroyed != null)
                AllEnemyInSceneDestroyed.Invoke();
        }
    }

    public void AddListerToEnemySpawnStartUnityEvent(UnityAction unityAction) => EnemySpawnStart.AddListener(unityAction);
    public void AddListenerToAllEnemyDestroyedUnityEvent(UnityAction unityAction) => AllEnemyInSceneDestroyed.AddListener(unityAction);
}