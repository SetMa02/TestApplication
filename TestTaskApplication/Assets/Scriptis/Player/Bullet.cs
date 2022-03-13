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
        public float CoolDown = 5;
        public float Damage = 35;
        public Transform Point;
        public float Speed = 50;

        public UnityAction<Bullet> BulletDestroyed;

        private void Update()
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            BulletDestroyed?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}