using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Waypoint : MonoBehaviour
{
    public UnityAction<Transform> WaypointsReached;
    public UnityAction TargetElemanated;
    
    [SerializeField] private List<EnemyController> _enemies;

    private void OnEnable()
    {
        foreach (var enemie in _enemies)
        {
            enemie.EnemieDie += EnemieDie;
        }
    }

    private void OnDisable()
    {
        foreach (var enemie in _enemies)
        {
            enemie.EnemieDie -= EnemieDie;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("StartHuntPlayer");
            WaypointsReached?.Invoke(other.transform);
        }
    }

    private void EnemieDie(EnemyController enemie)
    {
        _enemies.Remove(enemie);
        enemie.gameObject.SetActive(false);
        if (_enemies.Any() == false)
        {
            TargetElemanated?.Invoke();
        }
    }
}
