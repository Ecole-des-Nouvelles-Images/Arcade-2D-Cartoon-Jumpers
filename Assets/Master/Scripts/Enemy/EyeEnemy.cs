namespace Master.Scripts.Enemy
{
    public class EyeEnemy : Enemy
    {
      /* Move déplacé dans la classe parente
        public override void Move(IMoveCommand moveCommand)
        {
            if (moveCommand != null)
            {
                Vector2 _destination = moveCommand.Execute(transform); // récupère la destination de l'instance du SO
                Vector2 _direction = (_destination - (Vector2)transform.position);
                transform.Translate(_direction * (EnemySpeed * Time.deltaTime));
            }
        }
        */
    }
}
