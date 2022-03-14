using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Waypoint : MonoBehaviour
{
    public UnityAction WaypointsReached;
    public UnityAction TargetElemanated;
    
    [SerializeField] private List<EnemyController> _enemies;

    private void FixedUpdate()
    {
        CheckEnemies();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("StartHuntPlayer");
            WaypointsReached?.Invoke();
        }
    }
    
    private void CheckEnemies()
    {
        if(_enemies.Any() == false)
        {
            TargetElemanated?.Invoke();
            gameObject.SetActive(false);
        }
        else
        {
            foreach (var enemie in _enemies.ToList())
            {
                if (enemie.IsAlive == false)
                {
                    _enemies.Remove(enemie);
                }
            }
        }
    }
}
