using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Waypoint : MonoBehaviour
{
    public UnityAction<Transform> WaypointsReached;
    public UnityAction TargetElemanated;
    
    [SerializeField] private List<EnemyController> _enemies;

    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("StartHuntPlayer");
            WaypointsReached?.Invoke(other.transform);
        }
    }
}
