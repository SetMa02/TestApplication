 using System;
 using System.Collections;
using System.Collections.Generic;
 using Scriptis.Player;
 using UnityEngine;
 using UnityEngine.AI;

 [RequireComponent(typeof(NavMeshAgent))]
 [RequireComponent(typeof(Rigidbody))]
 [RequireComponent(typeof(Animator))]
 [RequireComponent(typeof(PlayerShooting))]
 [RequireComponent(typeof(PlayerRotation))]
 public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _waypoints;

    private NavMeshAgent _agent;
    private int _currentWaypoint;
    private Animator _animator;
    private PlayerRotation _playerRotation;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _playerRotation = GetComponent<PlayerRotation>();
        _currentWaypoint = -1;
        MoveToNextWaypoint();
        PlayerMove();
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
        PlayerMove();
        _currentWaypoint++;
        _agent.SetDestination(_waypoints[_currentWaypoint].gameObject.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Waypoint"))
        {
            PlayerStop();
            _playerRotation._rotateDirection = other.transform;
            _playerRotation.isRotate = true;
            Debug.Log("Player stop");
        }
    }

    private void PlayerStop()
    {
        _animator.Play("idle");
    }

    private void PlayerMove()
    {
        _animator.Play("Walk");
    }
}
