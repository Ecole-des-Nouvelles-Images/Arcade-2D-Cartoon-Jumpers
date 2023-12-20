using UnityEngine;

using EnemyComponent = Master.Scripts.Enemy.Enemy;

namespace Master.Scripts.SO.Commands
{
    [CreateAssetMenu(fileName = "FollowCommand", menuName = "New Follow Command")]
    public class FollowCommand : CommandSO
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _followingTriggerDistance;
        
        //TODO PAS BIEN
        private Vector2 _startingPosition;
        private Vector2 _destination;

        public override void Setup(EnemyComponent enemyComponent)
        {
            enemyComponent.Memory[(this, "startingPosition")] = (Vector2)enemyComponent.transform.position;
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject == null)
            {
                Debug.LogError("Player Not Found in the Scene");
                return;
            }

            enemyComponent.Memory[(this, "playerTransform")] = playerObject.transform;
        }

        public override void Execute(EnemyComponent enemyComponent)
        {
            if (_startingPosition == Vector2.zero) _startingPosition = enemyComponent.transform.position;
            Transform playerTransform = enemyComponent.Memory[(this, "playerTransform")] as Transform;
            if (playerTransform == null)
            {
                Debug.LogError("Player transform not found in memory");
                return;
            }

            _destination = playerTransform.position;
            float distanceToPlayer = Vector2.Distance(enemyComponent.transform.position, _destination);
            if (distanceToPlayer <= _followingTriggerDistance)
            {
                Vector2 direction = (_destination - (Vector2)enemyComponent.transform.position).normalized;
                enemyComponent.transform.Translate(direction * ((enemyComponent.Speed + _speed) * Time.deltaTime));
            }
        }

        public override bool IsFinished(EnemyComponent enemyComponent)
        {
            Vector2 startingPosition = (Vector2)enemyComponent.Memory[(this, "startingPosition")];
            Vector2 currentDestination = startingPosition + _destination;
            float distance = Vector2.Distance(currentDestination, enemyComponent.transform.position);

            return enemyComponent.HasCollidedWithPlayer;
        }

        public override void CleanUp(EnemyComponent enemyComponent)
        {
            enemyComponent.Memory.Remove((this, "startingPosition"));

        }
    }
}
