using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerAttackZone : MonoBehaviour
{
    private List<Enemy> _enemyInAttackZone = new List<Enemy>();

    public event Action AddEnemyInList;

    public Enemy ClosestEnemy { get; private set; } = null;

    private void Awake() => GetComponent<Collider>().isTrigger = true;
    private void Start()
    {
        GlobalStatesWhenPlayerDied.Instance.AddBehaviourInListToSetEnebledFalseWhenPlayerDied(this);
        GlobalStatesIfPlayerWin.Instance.AddBehaviourInListToSetEnebledFalseWhenPlayerWin(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _enemyInAttackZone.Add(enemy);
            GetClosestEnemyInAttackZone();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            GetClosestEnemyInAttackZone();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _enemyInAttackZone.Remove(enemy);
            GetClosestEnemyInAttackZone();
        }
    }
    private void GetClosestEnemyInAttackZone()
    {
        if (_enemyInAttackZone.Count > 0)
        {
            if (_enemyInAttackZone.First() == null)
                _enemyInAttackZone.Remove(_enemyInAttackZone.First());

            ClosestEnemy = _enemyInAttackZone.First();

            if (AddEnemyInList != null)
                AddEnemyInList.Invoke();
        }
    }
}
