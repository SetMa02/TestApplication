using System;
using System.Collections;
using System.Collections.Generic;
using Scriptis.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    public bool IsAlive = true;
    
    [SerializeField] private float health = 100;
    [SerializeField]private Image _healthBar;
    
    private Animator _animator;
    private Image[] _images;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _images = GetComponentsInChildren<Image>();
    }
    

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
        _healthBar.fillAmount = health/100;
        if (health <= 0)
        {
            foreach (var image in _images)
            {
                image.gameObject.SetActive(false);
            }
            Debug.Log("enemie die");
            IsAlive = false;
            _animator.enabled = false;
        }
    }
}
