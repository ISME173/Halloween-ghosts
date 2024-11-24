using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkingPoints : MonoBehaviour
{
    private static EnemyWalkingPoints _instance;

    private List<Transform> _enemyWalkingPoints = new List<Transform>();

    public static EnemyWalkingPoints Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<EnemyWalkingPoints>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<EnemyWalkingPoints>();
                    singletonObject.name = typeof(EnemyWalkingPoints).ToString();
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

        Transform[] points = GetComponentsInChildren<Transform>();
        foreach (Transform t in points)
            _enemyWalkingPoints.Add(t);
    }

    public List<Transform> GetEnemyWalkingPoints()
    {
        return _enemyWalkingPoints;
    }
}
