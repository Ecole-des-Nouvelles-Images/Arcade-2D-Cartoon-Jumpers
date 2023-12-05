using Alex.Scripts.Enemies;

namespace Alex.Scripts.Enemies
{
    public class EyeEnemy : Enemy
    {
      
        public override void Move(IMoveCommand moveCommand)
        {
            moveCommand.Execute(transform, EnemySpeed);
        }
    }
}