using System;
using System.Collections;
using System.Collections.Generic;
using Scriptis.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public bool IsAlive = true;
    
    [SerializeField] private float health = 100;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            TakeDamage(bullet.Damage);
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {
            Debug.Log("enemie die");
            IsAlive = false;
            gameObject.SetActive(false);
        }
    }
}
