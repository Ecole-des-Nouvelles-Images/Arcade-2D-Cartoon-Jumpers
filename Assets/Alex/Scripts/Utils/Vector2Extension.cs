using UnityEngine;

namespace Alex.Scripts.Utils
{
    public static class Vector2Extension
    {
        public static float Length(this Vector2 vector2) {
            return Mathf.Sqrt(vector2.x * vector2.x + vector2.y * vector2.y);
        }
    }
}