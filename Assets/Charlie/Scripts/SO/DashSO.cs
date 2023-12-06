using Charlie.Scripts.Player;
using UnityEngine;

namespace Charlie.Scripts.SO
{
    [CreateAssetMenu(menuName = "Create New Dash Type", fileName = "Dash", order = 3)]
    public class DashSO: ScriptableObject
    {
        [Header("Base Stats")]
        public int Power;
        public float Velocity = 80f;
        public float Cooldown = 1f;

        public Dash ConstructDashObject()
        {
            return new Dash(Power, Velocity, Cooldown);
        }
    }
}
