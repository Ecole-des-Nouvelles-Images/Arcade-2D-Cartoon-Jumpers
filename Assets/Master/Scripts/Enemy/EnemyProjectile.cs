using System;
using UnityEngine;
using EnemyComponent = Master.Scripts.Enemy.Enemy;
using PlayerComponent = Master.Scripts.Player.Player;

namespace Master.Scripts.Enemy
{
    public class EnemyProjectile : MonoBehaviour
    {
        private EnemyComponent _origin;
        private SpriteRenderer _spriteRenderer;
        
        private Vector2 _direction;
        private float _velocity;
        private Quaternion _angle;
        
        public void Initialize(EnemyComponent origin, Vector2 fireDirection, float velocity)
        {
            _origin = origin;
            _direction = fireDirection;
            _velocity = velocity;
           // _angle = Quaternion.AngleAxis(Mathf.Atan2(fireDirection.x, fireDirection.y), Vector3.forward);
           // float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
        }

        private void OnEnable()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            transform.rotation = _angle;
        }

        private void Update()
        {
            GetComponent<Rigidbody2D>().velocity = _direction * _velocity;
            _spriteRenderer.flipX = _direction.x > 0;
            //transform.rotation = _angle;
            //transform.Translate(_direction * (_velocity * Time.deltaTime) );
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            _origin.OnAttack?.Invoke(_origin.Power);
            Destroy(this.gameObject);
        }
    }
}
