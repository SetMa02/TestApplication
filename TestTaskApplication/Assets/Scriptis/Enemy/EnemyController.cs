using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public UnityAction<EnemyController> EnemieDie;
    
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Waypoint") )
        {
            
        }
        else
        {
            EnemieDie.Invoke(this);
        }
    }
}
