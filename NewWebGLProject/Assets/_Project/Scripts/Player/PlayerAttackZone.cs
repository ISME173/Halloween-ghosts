using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerAttackZone : MonoBehaviour
{
    private List<Enemy> _enemyInAttackZone = new List<Enemy>();

    public event Action AddEnemyInListClosestEnemy, RemoveEnemyInListClosestEnemy;

    public Enemy ClosestEnemy { get; private set; } = null;

    private void Awake() => GetComponent<Collider>().isTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _enemyInAttackZone.Add(enemy);
            ClosestEnemy = SearchClosestEnemyInAttackZone();

            if (AddEnemyInListClosestEnemy != null)
                AddEnemyInListClosestEnemy.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _enemyInAttackZone.Remove(enemy);
            ClosestEnemy = SearchClosestEnemyInAttackZone();

            if (ClosestEnemy != null && RemoveEnemyInListClosestEnemy != null)
                RemoveEnemyInListClosestEnemy.Invoke();
                
        }
    }

    private Enemy SearchClosestEnemyInAttackZone()
    {
        float minDistanceToEnemy = 0;
        Enemy closestEnemy = null;

        for (int i = 0; i < _enemyInAttackZone.Count; i++)
        {
            //if (_enemyInAttackZone[i] == null)
            //{
            //    _enemyInAttackZone.RemoveAt(i);
            //    continue;
            //}

            if (i == 0)
            {
                minDistanceToEnemy = Vector3.Distance(transform.position, _enemyInAttackZone[i].transform.position);
                closestEnemy = _enemyInAttackZone[i];
                continue;
            }

            float distanceToEnemy = Vector3.Distance(transform.position, _enemyInAttackZone[i].transform.position);
            if (distanceToEnemy < minDistanceToEnemy)
            { 
                minDistanceToEnemy = distanceToEnemy;
                closestEnemy = _enemyInAttackZone[i];
            }
        }
        return closestEnemy;
    }
}
