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

        public override void Setup(EnemyComponent enemy) {
            enemy.Memory[(this, "startingPosition")] = (Vector2) enemy.transform.position;
        }
        
        public override void Execute(EnemyComponent enemy)
        {
            if (_startingPosition == Vector2.zero) _startingPosition = enemy.transform.position;
            //Vector2 currentDestination = _startingPosition + _destination - (Vector2)enemy.transform.position; // recalculer la destination à chaque frame à partir de la position de départ 
            Vector2 direction = _destination.normalized; // si on ne normalise pas, il baisse la vitesse à l'approche de la destination sans jamais l'atteindre
            enemy.transform.Translate(direction * ((enemy.EnemySpeed + _speed) * Time.deltaTime));
        }

        public override bool IsFinished(EnemyComponent enemy)
        {
            Vector2 startingPosition = (Vector2) enemy.Memory[(this, "startingPosition")];
            Vector2 currentDestination = startingPosition + _destination;
            float distance = Vector2.Distance(currentDestination, enemy.transform.position);
            //float normalizedDirection = (currentDestination - (Vector2) enemy.transform.position).Length();
            //Debug.Log(normalizedDirection);
            return distance <= 1f;
        }

        public override void CleanUp(EnemyComponent enemy) {
            enemy.Memory.Remove((this, "startingPosition"));
        }
    }
}
