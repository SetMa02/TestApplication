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
    [SerializeField] private float _bulletLifeTime;
    
    private List<Bullet> _bullets = new List<Bullet>() { };
    
    private void Awake()
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
                Shoot(hit.point);
            }
        }
    }

    private void Shoot(Vector3 point)
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
            Vector3 shootPoint = new Vector3(point.x, currentBullet.transform.position.y,
                point.z);
            
            
            currentBullet.transform.LookAt(shootPoint);
            currentBullet.Speed = _bulletSpeed;
            currentBullet.BulletLifeTime = _bulletLifeTime;
            currentBullet.gameObject.SetActive(true);
            currentBullet.isReady = false;
        }

        if (currentBullet == null)
        {
            Debug.Log("Out of ammo");
        }
    }
    private void OnBulletCollision(Bullet bullet)
    {
        Debug.Log("Bullet on cooldown");
        StartCoroutine(BulletCoolDown(bullet));
    }

    private IEnumerator BulletCoolDown(Bullet bullet)
    {
        Debug.Log("Bullet on cooldown");
        bullet.transform.position = _container.transform.position;
        yield return new WaitForSeconds(_cooldown);
        bullet.isReady = true;
        Debug.Log("coolDown is over");

    }
    
}
