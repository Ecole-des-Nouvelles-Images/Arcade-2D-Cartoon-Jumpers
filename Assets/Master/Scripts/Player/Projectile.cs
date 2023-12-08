using UnityEngine;

namespace Master.Scripts.Player
{
    public class Projectile
    {
        public GameObject Prefab;
        
        public int Power;
        public float Cooldown;
        public int MaxCapacity;
        public float RecoilVelocity;
        public float Velocity;

        public int Capacity { get; set; }

        public Projectile(GameObject projectilePrefab, int power, float cooldown, int capacity, float recoilVelocity, float velocity)
        {
            Prefab = projectilePrefab;
            Power = power;
            Cooldown = cooldown;
            MaxCapacity = capacity;
            Capacity = MaxCapacity;
            RecoilVelocity = recoilVelocity;
            Velocity = velocity;
        }
    }
}
