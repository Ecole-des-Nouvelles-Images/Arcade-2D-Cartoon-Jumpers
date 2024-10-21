using UnityEngine;
using Master.Scripts.PlayerManagement;

namespace Master.Scripts.SO
{
    [CreateAssetMenu(menuName = "Player Action/New Dash Type", fileName = "Dash", order = 3)]
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
