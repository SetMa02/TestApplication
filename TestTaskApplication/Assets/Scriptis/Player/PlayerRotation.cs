using System;
using UnityEngine;

namespace Scriptis.Player
{
    public class PlayerRotation : MonoBehaviour
    {
        public bool isRotate = false;
        public Transform _rotateDirection;

        [SerializeField] private float _rotationSpeed;
        private void FixedUpdate()
        {
            if (isRotate == true)
            {
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, _rotateDirection.rotation,
                                    _rotationSpeed * Time.deltaTime);
                if (gameObject.transform.rotation == _rotateDirection.rotation)
                {
                    isRotate = false;
                }
            }
        }
    }
}