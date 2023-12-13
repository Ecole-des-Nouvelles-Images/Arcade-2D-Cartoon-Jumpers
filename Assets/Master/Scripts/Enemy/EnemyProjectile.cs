using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Master.Scripts.Enemy
{
    public class EnemyProjectile : MonoBehaviour
    {
        private Vector2 _direction;
        private float _speed;

       public void Initialize(Vector2 fireDirection, float projectileSpeed)
        {
            _direction = fireDirection;
            _speed = projectileSpeed;
        }

        private void Update()
        {
            transform.Translate(_direction * (_speed * Time.deltaTime));
        }
    }
}