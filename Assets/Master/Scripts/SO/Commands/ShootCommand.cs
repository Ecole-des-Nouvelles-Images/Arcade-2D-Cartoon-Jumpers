using UnityEngine;
using Master.Scripts.Enemy;
using EnemyComponent = Master.Scripts.Enemy.Enemy;

namespace Master.Scripts.SO.Commands
{
    [CreateAssetMenu(fileName = "ShootCommand", menuName = "New ShootCommand Command")]
    public class ShootCommand : CommandSO
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private int firePower = 10;
        [SerializeField] private float fireSpeed = 5f;
        [SerializeField] private float _shootingTriggerDistance;
        [SerializeField] private float _fireTime;
        private float nextFireTime;

        public override void Setup(EnemyComponent enemy)
        {
            EnemyCtx = enemy;
            enemy.Memory[(this, "_fireTimeOut")] = Time.time + _fireTime;
            nextFireTime = Time.time + 1f / fireRate;

            enemy.Memory[(this, "_playerTransform")] = enemy.PlayerReference.transform;
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
            if (fireRate != 0)
            {
                if (Time.time >= nextFireTime && distanceToPlayer <= _shootingTriggerDistance)
                {
                    Fire(EnemyCtx.PlayerReference.transform);
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
            else
            {
                Fire(EnemyCtx.PlayerReference.transform);
                EnemyCtx.Memory[(this, "_fireTimeOut")] = 0f;
            }
        }

        private void Fire(Transform playerTransform)
        {
            Vector2 position = EnemyCtx.transform.position;
            GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
            Vector2 fireDirection = ((Vector2)playerTransform.position - position).normalized;
            projectile.GetComponent<EnemyProjectile>().Initialize(fireDirection, firePower, fireSpeed);
        }

        public override bool IsFinished()
        {
            float _fireTimeOut = (float)EnemyCtx.Memory[(this, "_fireTimeOut")];
            return Time.time >= _fireTimeOut;
        }

        public override void CleanUp()
        {
            EnemyCtx.Memory.Remove((this, "_fireTimeOut"));
            EnemyCtx.Memory.Remove((this, "_playerTransform"));
        }
    }
}