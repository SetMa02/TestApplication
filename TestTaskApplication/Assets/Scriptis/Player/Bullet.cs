using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptis.Player
{
    public class Bullet : MonoBehaviour
    {
        public bool isReady = true;
        public float Damage = 35;
        public float Speed = 50;
        public float BulletLifeTime = 2;

        public UnityAction<Bullet> BulletDestroyed;

        private void Update()
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            BulletLifeTime -= Time.deltaTime;
            if (BulletLifeTime <= 0)
            {
                TurnBulletOff();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            TurnBulletOff();
        }
        
        private void TurnBulletOff()
        {
            BulletDestroyed?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}