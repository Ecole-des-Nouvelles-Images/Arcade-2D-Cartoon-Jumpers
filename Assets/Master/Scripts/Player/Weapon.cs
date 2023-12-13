using System;
using UnityEngine;
using Object = UnityEngine.Object;

using Master.Scripts.Common;

namespace Master.Scripts.Player
{
    public class Weapon
    {
        public readonly GameObject Prefab;
        
        public int Power;
        public float Cooldown;
        public int MaxCapacity;
        public float RecoilVelocity;
        public float ProjectileVelocity;

        public int Capacity { get; set; }

        private readonly Transform _projectileOrigin;

        public Weapon(GameObject projectilePrefab, int power, float cooldown, int capacity, float recoilVelocity, float projectileVelocity, Transform projectileOrigin)
        {
            Prefab = projectilePrefab;
            Power = power;
            Cooldown = cooldown;
            MaxCapacity = capacity;
            Capacity = MaxCapacity;
            RecoilVelocity = recoilVelocity;
            ProjectileVelocity = projectileVelocity;

            _projectileOrigin = projectileOrigin;
        }

        public void Shoot(Player playerCtx, Vector2 direction)
        {
            playerCtx.Rigidbody.velocity = Vector2.zero; // conserver le reset avant le tir ? 
            playerCtx.Rigidbody.AddForce(-direction * playerCtx.Weapon.RecoilVelocity, ForceMode2D.Impulse);

            Projectile projectile = Projectile.Create(playerCtx, playerCtx.transform.position, _projectileOrigin);

            if (projectile != null) {
                projectile.Prepare(playerCtx.Weapon);
                projectile.Fire(direction.normalized);
            }
            else
                throw new Exception("Cannot Instantiate shots ammunitons");

            playerCtx.Weapon.Capacity--;
        }
    }
}
