using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Master.Scripts.SO;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Master.Scripts.SO.Commands
{
    [CreateAssetMenu(fileName = "FollowCommand", menuName = "New Follow Command")]
    public class FollowCommand : CommandSO
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _followingTriggerDistance;
        private Vector2 _startingPosition;
        private Vector2 _destination;

        public override void Setup(Enemy.Enemy enemy)
        {
            enemy.Memory[(this, "startingPosition")] = (Vector2)enemy.transform.position;
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
            if (_startingPosition == Vector2.zero) _startingPosition = enemy.transform.position;
            Transform playerTransform = enemy.Memory[(this, "playerTransform")] as Transform;
            if (playerTransform == null)
            {
                Debug.LogError("Player transform not found in memory");
                return;
            }

            _destination = playerTransform.position;
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, _destination);
            if (distanceToPlayer <= _followingTriggerDistance)
            {
                Vector2 direction = (_destination - (Vector2)enemy.transform.position).normalized;
                enemy.transform.Translate(direction * ((enemy.EnemySpeed + _speed) * Time.deltaTime));
            }
        }

        public override bool IsFinished(Enemy.Enemy enemy)
        {
            Vector2 startingPosition = (Vector2)enemy.Memory[(this, "startingPosition")];
            Vector2 currentDestination = startingPosition + _destination;
            float distance = Vector2.Distance(currentDestination, enemy.transform.position);

            return enemy.HasCollidedWithPlayer;
        }

        public override void CleanUp(Enemy.Enemy enemy)
        {
            enemy.Memory.Remove((this, "startingPosition"));

        }
    }
}