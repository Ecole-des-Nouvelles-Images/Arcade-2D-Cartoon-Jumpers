using UnityEngine;

using Master.Scripts.Enemy;
using EnemyComponent = Master.Scripts.Enemy.Enemy;

namespace Master.Scripts.SO.Commands
{
    [CreateAssetMenu(fileName = "ShootCommand", menuName = "New ShootCommand Command")]
    public class ShootCommand : CommandSO
    {
        [Header("Reference")]
        [SerializeField] private GameObject _projectilePrefab;
        
        [Header("Values")]
        [SerializeField] private float _velocity = 5f;
        [SerializeField] private float _triggerDistance;

        public override void Setup(EnemyComponent enemyComponent)
        {
            enemyComponent.Memory[(this, "_hasShooted")] = false;
            enemyComponent.Memory[(this, "_playerTransform")] = enemyComponent.PlayerReference.transform;
        }

        public override void Execute(EnemyComponent enemyComponent)
        {
            Transform playerTransform = enemyComponent.Memory[(this, "_playerTransform")] as Transform;
            if (playerTransform == null)
            {
                Debug.LogError("Player transform not found in memory");
                return;
            }

            float distanceToPlayer = Vector2.Distance(enemyComponent.transform.position, playerTransform.position);
            if (distanceToPlayer <= _triggerDistance) 
                Fire(enemyComponent, enemyComponent.PlayerReference.transform);
        }

        private void Fire(EnemyComponent enemyComponent, Transform playerTransform)
        {
            Vector2 position = enemyComponent.transform.position;
            GameObject projectile = Instantiate(_projectilePrefab, position, Quaternion.identity);
            Vector2 fireDirection = ((Vector2)playerTransform.position - position).normalized;
            projectile.GetComponent<EnemyProjectile>().Initialize(enemyComponent, fireDirection, _velocity);
            
            // enemyComponent.Memory[(this, "_hasShooted")] = true;
        }

        public override bool IsFinished(EnemyComponent enemyComponent)
        {
            return true;
        }

        public override void CleanUp(EnemyComponent enemyComponent)
        {
            enemyComponent.Memory.Clear();
        }
    }
}
