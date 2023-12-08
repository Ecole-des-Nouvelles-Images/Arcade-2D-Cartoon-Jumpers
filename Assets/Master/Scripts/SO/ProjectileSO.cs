using UnityEngine;
using Master.Scripts.Player;

namespace Master.Scripts.SO
{
    [CreateAssetMenu(menuName = "Player Action/New Projectile Type", fileName = "Projectile", order = 4)]
    public class ProjectileSO: ScriptableObject
    {
        [Header("Projectile prefab")]
        public GameObject ProjectilePrefab;
        
        [Header("Base Stats")]
        public int Power;
        public float Cooldown = 1f;
        public int Capacity;

        [Header("Physical velocities")]
        public float RecoilVelocity = 40f;
        public float ProjectileVelocity;
        
        public Projectile ConstructProjectileObject()
        {
            return new Projectile(ProjectilePrefab, Power, Cooldown, Capacity, RecoilVelocity, ProjectileVelocity);
        }
    }
}
