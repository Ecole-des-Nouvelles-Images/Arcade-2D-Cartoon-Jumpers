using UnityEngine;
using EnemyComponent = Master.Scripts.Enemy.Enemy;
using PlayerComponent = Master.Scripts.Player.Player;

namespace Master.Scripts.Enemy
{
    public class EnemyProjectile : MonoBehaviour
    {
        private EnemyComponent _origin;
        
        private Vector2 _direction;
        private float _velocity;
        
        public void Initialize(EnemyComponent origin, Vector2 fireDirection, float velocity)
        {
            _origin = origin;
            _direction = fireDirection;
            _velocity = velocity;
        }

        private void Update()
        {
            transform.Translate(_direction * (_velocity * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            _origin.OnAttack.Invoke(_origin.Power);
            Destroy(this.gameObject);
        }
    }
}
