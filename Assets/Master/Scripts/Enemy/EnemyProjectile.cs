using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyComponent = Master.Scripts.Enemy.Enemy;
using PlayerComponent = Master.Scripts.Player.Player;

namespace Master.Scripts.Enemy
{
    public class EnemyProjectile : MonoBehaviour
    {
        private Vector2 _direction;
        private float _speed;
        private int _power;
       
        private void OnEnable()
        {
            PlayerComponent.OnDamageTakenFromProjectile += DealProjectileDamage;
        }

        private void OnDisable()
        {
            PlayerComponent.OnDamageTakenFromProjectile -= DealProjectileDamage;
        }
       public void Initialize(Vector2 fireDirection, int firePower, float fireSpeed)
        {
            _direction = fireDirection;
            _speed = fireSpeed;
            _power = firePower;
        }

        private void Update()
        {
            transform.Translate(_direction * (_speed * Time.deltaTime));
        }
        
        
        private void DealProjectileDamage(PlayerComponent ctx) // TODO: Test
        {
            Debug.Log($"Player damaged from Projectile");
            ctx.HealthPoint -= _power;
            Destroy(gameObject);
        }
    }
}