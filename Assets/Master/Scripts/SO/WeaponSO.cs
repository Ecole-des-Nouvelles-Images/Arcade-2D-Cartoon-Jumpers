using UnityEngine;
using Master.Scripts.PlayerManagement;

namespace Master.Scripts.SO
{
    [CreateAssetMenu(menuName = "Player Action/New Projectile Type", fileName = "Projectile", order = 4)]
    public class WeaponSO: ScriptableObject
    {
        [Header("References")]
        public GameObject ProjectilePrefab;
        public string ObjectContainerName;
        
        [Header("Base Stats")]
        public int Power;
        public float Cooldown = 1f;
        public int Capacity;

        [Header("Physical velocities")]
        public float RecoilVelocity = 40f;
        public float ProjectileVelocity;
        
        public Weapon ConstructWeaponObject()
        {
            Transform projectileOrigin = GameObject.Find(ObjectContainerName).transform;
            return new Weapon(ProjectilePrefab, Power, Cooldown, Capacity, RecoilVelocity, ProjectileVelocity, projectileOrigin);
        }
    }
}
