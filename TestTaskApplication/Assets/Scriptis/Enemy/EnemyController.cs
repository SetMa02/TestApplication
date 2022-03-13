using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Waypoint _waypoint;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _waypoint.WaypointsReached += MoveToPlayer;
    }

    private void OnDisable()
    {
        _waypoint.WaypointsReached -= MoveToPlayer;
    }

    private void MoveToPlayer(Transform player)
    {
        _agent.SetDestination(player.position);
    }
}
