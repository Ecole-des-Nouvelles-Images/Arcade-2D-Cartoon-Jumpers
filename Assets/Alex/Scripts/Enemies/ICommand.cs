using UnityEngine;

namespace Alex.Scripts.Enemies
{
    public interface IMoveCommand
    {
        void Execute(Transform transform, float speed);
    }
}