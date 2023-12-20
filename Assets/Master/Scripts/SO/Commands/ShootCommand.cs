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

        public override void Setup(EnemyComponent enemy)
        {
            EnemyCtx = enemy;
            EnemyCtx.Memory[(this, "_hasShooted")] = false;
            EnemyCtx.Memory[(this, "_playerTransform")] = enemy.PlayerReference.transform;
        }

        public override void Execute()
        {
            Transform playerTransform = EnemyCtx.Memory[(this, "_playerTransform")] as Transform;
            if (playerTransform == null)
            {
                Debug.LogError("Player transform not found in memory");
                return;
            }

            float distanceToPlayer = Vector2.Distance(EnemyCtx.transform.position, playerTransform.position);
            if (distanceToPlayer <= _triggerDistance) 
                Fire(EnemyCtx.PlayerReference.transform);
        }

        private void Fire(Transform playerTransform)
        {
            Vector2 position = EnemyCtx.transform.position;
            GameObject projectile = Instantiate(_projectilePrefab, position, Quaternion.identity);
            Vector2 fireDirection = ((Vector2)playerTransform.position - position).normalized;
            projectile.GetComponent<EnemyProjectile>().Initialize(EnemyCtx, fireDirection, _velocity);
            
            // EnemyCtx.Memory[(this, "_hasShooted")] = true;
        }

        public override bool IsFinished()
        {
            return true;
        }

        public override void CleanUp()
        {
            EnemyCtx.Memory.Clear();
        }
    }
}
