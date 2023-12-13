using System.Collections;
using System.Collections.Generic;
using Master.Scripts.Enemy;
using UnityEngine;


namespace Master.Scripts.SO.Commands
{
    [CreateAssetMenu(fileName = "ShootCommand", menuName = "New ShootCommand Command")]

    public class ShootCommand : CommandSO
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private float firePower = 10f;
        [SerializeField] private float _shootingTriggerDistance;
        [SerializeField] private float _fireTime;
        private float nextFireTime;
        
        public override void Setup(Enemy.Enemy enemy)
        {
            enemy.Memory[(this, "_fireTimeOut")] = Time.time + _fireTime;
            nextFireTime = Time.time + 1f / fireRate;
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject == null)
            {
                Debug.LogError("Player Not Found in the Scene");
                return;
            }

            enemy.Memory[(this, "playerTransform")] = playerObject.transform;
        }

        public override void Execute(Enemy.Enemy enemy)
        {
            Transform playerTransform = enemy.Memory[(this, "playerTransform")] as Transform;
            if (playerTransform == null)
            {
                Debug.LogError("Player transform not found in memory");
                return;
            }

            float distanceToPlayer = Vector2.Distance(enemy.transform.position, playerTransform.position);
            if (Time.time >= nextFireTime && distanceToPlayer <= _shootingTriggerDistance)
            {
                Fire(enemy, playerTransform);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        private void Fire(Enemy.Enemy enemy, Transform playerTransform)
        {
           Vector2 position = enemy.transform.position;
            GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
            Vector2 fireDirection = ((Vector2)playerTransform.position - position).normalized;
            projectile.GetComponent<EnemyProjectile>().Initialize(fireDirection, firePower);
        }
        public override bool IsFinished(Enemy.Enemy enemy)
        {
            float _fireTimeOut = (float)enemy.Memory[(this, "_fireTimeOut")];
            return Time.time >= _fireTimeOut;
        }

        public override void CleanUp(Enemy.Enemy enemy)
        {
            enemy.Memory.Remove((this, "_fireTimeOut"));
        }
    }
}
