using System;
using System.Collections;
using System.Collections.Generic;
using Scriptis.Player;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private int _bulletCount;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private GameObject _container;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private int _cooldown;
    
    private List<Bullet> _bullets = new List<Bullet>() { };
    
    private void Start()
    {
        for (int i = 1; i <= _bulletCount; i++)
        {
           _bullets.Add(Instantiate(_bulletPrefab, _container.transform, false));
           _bullets[i-1].gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        foreach (var bullet in _bullets)
        {
            bullet.BulletDestroyed += OnBulletCollision;
        }
    }

    private void OnDisable()
    {
        foreach (var bullet in _bullets)
        {
            bullet.BulletDestroyed -= OnBulletCollision;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast( _mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Shoot(hit.transform);
            }
        }
    }

    private void Shoot(Transform point)
    {
        Bullet currentBullet = null;
        foreach (var bullet in _bullets)
        {
            if (bullet.isReady == true)
            {
                currentBullet = bullet;
                break;
            }
        }
        if (currentBullet != null)
        {
            Vector3 shootPoint = new Vector3(point.transform.position.x, currentBullet.transform.position.y,
                point.transform.position.z);
            currentBullet.transform.LookAt(shootPoint);
            currentBullet.Speed = _bulletSpeed;
            currentBullet.Point = point;
            currentBullet.gameObject.SetActive(true);
            currentBullet.isReady = false;
        }

        if (currentBullet == null)
        {
            
        }
    }

    private void OnBulletCollision(Bullet bullet)
    {
        StartCoroutine(BulletCoolDown(bullet));
    }

    private IEnumerator BulletCoolDown(Bullet bullet)
    {
        bullet.transform.position = _container.transform.position;
        yield return new WaitForSeconds(_cooldown);
        bullet.isReady = true;
    }
    
}
