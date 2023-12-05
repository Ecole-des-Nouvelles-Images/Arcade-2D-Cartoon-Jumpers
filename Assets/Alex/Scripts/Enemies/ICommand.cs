using UnityEngine;

namespace Alex.Scripts.Enemies
{
    public interface IMoveCommand
    {
        Vector2 Execute(Transform transform);
    }
}