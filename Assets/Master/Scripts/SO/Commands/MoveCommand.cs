using UnityEngine;

using EnemyComponent = Master.Scripts.Enemy.Enemy;

namespace Master.Scripts.SO.Commands
{
    [CreateAssetMenu(fileName = "Command", menuName = "Patterns Command/New Move Command")]
    public class MoveCommand : CommandSO
    {
        [SerializeField] private Vector2 _destination; // Renseigne la destination 
        [SerializeField] private float _speed; // Add some speed to the Enemy's own. 
        
        public override void Setup(EnemyComponent enemyComponent)
        {
            enemyComponent.Memory[(this, "enemyComponent")] = enemyComponent;
            enemyComponent.Memory[(this, "startingPosition")] = (Vector2) enemyComponent.transform.position;
        }
        
        public override void Execute(EnemyComponent enemyComponent)
        {
            Vector2 direction = _destination.normalized; 
            enemyComponent.SpriteRenderer.flipX = direction.x > 0;
            enemyComponent.transform.Translate(direction * ((enemyComponent.Speed + _speed) * Time.deltaTime));
        }

        public override bool IsFinished(EnemyComponent enemyComponent)
        {
            Vector2 startingPosition = (Vector2) enemyComponent.Memory[(this, "startingPosition")];
            Vector2 currentDestination = startingPosition + _destination;
            float distance = Vector2.Distance(currentDestination, enemyComponent.transform.position);
            return distance <= 1f;
        }

        public override void CleanUp(EnemyComponent enemyComponent) {
            enemyComponent.Memory.Remove((this, "startingPosition"));
        }
    }
}
