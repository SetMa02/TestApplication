 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.AI;

 [RequireComponent(typeof(NavMeshAgent))]
 [RequireComponent(typeof(Rigidbody))]
 public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _waypoints;

    private NavMeshAgent _agent;
    private int _currentWaypoint;


    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentWaypoint = -1;
        MoveToNextWaypoint();
      
    }
    

    private void OnEnable()
    {
        foreach (var waypoint in _waypoints)
        {
            waypoint.TargetElemanated += MoveToNextWaypoint;
        }
    }
    private void OnDisable()
    {
        foreach (var waypoint in _waypoints)
        {
            waypoint.TargetElemanated -= MoveToNextWaypoint;
        }
    }
    
    private void MoveToNextWaypoint()
    {
        _currentWaypoint++;
        _agent.SetDestination(_waypoints[_currentWaypoint].gameObject.transform.position);
    }
}
