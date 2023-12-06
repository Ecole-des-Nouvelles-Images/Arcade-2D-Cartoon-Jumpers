using Alex.Scripts.Enemies;
using UnityEngine;

namespace Alex.Scripts.Enemies
{
    [CreateAssetMenu(fileName = "New Move Command", menuName = "Move Command")]
    public class MoveCommand : Command
    {
        public Vector2 _destination; // Renseigne la destination 
        public float speed;

        public override void Execute(Enemy enemy)
        {
            enemy.transform.Translate(_destination * Time.deltaTime);
        }

        public override bool IsFinished(Enemy enemy)
        {
            Vector2 startingPosition = Vector2.zero;
            return Vector2.Distance(enemy.transform.position,startingPosition + _destination) < 0.01f;
        }
    }
}