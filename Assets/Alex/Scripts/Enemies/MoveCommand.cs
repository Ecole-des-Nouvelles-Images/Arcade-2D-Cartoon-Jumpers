using Alex.Scripts.Enemies;
using UnityEngine;

namespace Alex.Scripts.Enemies
{
    [CreateAssetMenu(fileName = "New Move Command", menuName = "Move Command")]
    public class MoveCommand : ScriptableObject, IMoveCommand
    {
        public Vector2 _direction;

        public void Execute(Transform transform, float speed)
        {
            transform.Translate(_direction * (speed * Time.deltaTime), Space.World);
        }
    }
}