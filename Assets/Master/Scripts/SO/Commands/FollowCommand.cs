using System.Collections;
using System.Collections.Generic;
using Master.Scripts.SO;
using UnityEngine;

namespace Master.Scripts.SO.Commands
{
    [CreateAssetMenu(fileName = "FollowCommand", menuName = "New Follow Command")]

    public class FollowCommand : CommandSO
    {
        [SerializeField] private float _speed;
        private Vector2 _startingPosition;
        
        public override void Setup(Enemy.Enemy enemy)
        {
            //enemy.Memory[(this, "startingPosition")] = (Vector2) enemy.transform.position;
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
           // if (_startingPosition == Vector2.zero) _startingPosition = enemy.transform.position;
           Transform playerTransform = enemy.Memory[(this, "playerTransform")] as Transform;
           if (playerTransform == null)
           {
               Debug.LogError("Player transform not found in memory");
               return;

           }
            Vector2 positionToFollow = playerTransform.position ;
            enemy.transform.Translate(positionToFollow * ((enemy.EnemySpeed + _speed )* Time.deltaTime));
        }

        public override bool IsFinished(Enemy.Enemy enemy)
        {
            return enemy.HasCollidedWithPlayer;
        }

        public override void CleanUp(Enemy.Enemy enemy)
        {
            throw new System.NotImplementedException();
        }
    }
}