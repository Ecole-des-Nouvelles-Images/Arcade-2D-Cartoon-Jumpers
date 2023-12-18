using UnityEngine;

using EnemyComponent = Master.Scripts.Enemy.Enemy;

namespace Master.Scripts.SO.Commands
{
    [CreateAssetMenu(fileName = "FollowCommand", menuName = "New Follow Command")]
    public class FollowCommand : CommandSO
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _followingTriggerDistance;
        private Vector2 _startingPosition;
        private Vector2 _destination;

        public override void Setup(EnemyComponent enemy)
        {
            EnemyCtx = enemy;
            EnemyCtx.Memory[(this, "startingPosition")] = (Vector2)EnemyCtx.transform.position;
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject == null)
            {
                Debug.LogError("Player Not Found in the Scene");
                return;
            }

            EnemyCtx.Memory[(this, "playerTransform")] = playerObject.transform;
        }

        public override void Execute()
        {
            if (_startingPosition == Vector2.zero) _startingPosition = EnemyCtx.transform.position;
            Transform playerTransform = EnemyCtx.Memory[(this, "playerTransform")] as Transform;
            if (playerTransform == null)
            {
                Debug.LogError("Player transform not found in memory");
                return;
            }

            _destination = playerTransform.position;
            float distanceToPlayer = Vector2.Distance(EnemyCtx.transform.position, _destination);
            if (distanceToPlayer <= _followingTriggerDistance)
            {
                Vector2 direction = (_destination - (Vector2)EnemyCtx.transform.position).normalized;
                EnemyCtx.transform.Translate(direction * ((EnemyCtx.Speed + _speed) * Time.deltaTime));
            }
        }

        public override bool IsFinished()
        {
            Vector2 startingPosition = (Vector2)EnemyCtx.Memory[(this, "startingPosition")];
            Vector2 currentDestination = startingPosition + _destination;
            float distance = Vector2.Distance(currentDestination, EnemyCtx.transform.position);

            return EnemyCtx.HasCollidedWithPlayer;
        }

        public override void CleanUp()
        {
            EnemyCtx.Memory.Remove((this, "startingPosition"));

        }
    }
}
