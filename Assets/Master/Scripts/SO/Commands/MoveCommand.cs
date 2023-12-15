using UnityEngine;

using EnemyComponent = Master.Scripts.Enemy.Enemy;

namespace Master.Scripts.SO.Commands
{
    [CreateAssetMenu(fileName = "Command", menuName = "Patterns Command/New Move Command")]
    public class MoveCommand : CommandSO
    {
        [SerializeField] private Vector2 _destination; // Renseigne la destination 
        [SerializeField] private float _speed; // Add some speed to the Enemy's own. 
        
        private Vector2 _startingPosition;
        
        public override void Setup(EnemyComponent enemy)
        {
            EnemyCtx = enemy;
            EnemyCtx.Memory[(this, "startingPosition")] = (Vector2) EnemyCtx.transform.position;
        }
        
        public override void Execute()
        {
            if (_startingPosition == Vector2.zero) _startingPosition = EnemyCtx.transform.position;
            Vector2 direction = _destination.normalized; 
            EnemyCtx.GetComponent<SpriteRenderer>().flipX = direction.x > 0;
            EnemyCtx.transform.Translate(direction * ((EnemyCtx.EnemySpeed + _speed) * Time.deltaTime));
        }

        public override bool IsFinished()
        {
            _startingPosition = (Vector2) EnemyCtx.Memory[(this, "startingPosition")];
            Vector2 currentDestination = _startingPosition + _destination;
            float distance = Vector2.Distance(currentDestination, EnemyCtx.transform.position);
            return distance <= 1f;
        }

        public override void CleanUp() {
            EnemyCtx.Memory.Remove((this, "startingPosition"));
        }
    }
}
