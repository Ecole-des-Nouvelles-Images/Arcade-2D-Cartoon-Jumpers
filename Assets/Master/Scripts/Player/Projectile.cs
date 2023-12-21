using UnityEngine;

namespace Master.Scripts.Player
{
    public class Projectile: MonoBehaviour
    {
        private Weapon _origin;
        private Rigidbody2D _rb;

        public static Projectile Create(Player ctx, Vector3 position, Transform container)
        {
            GameObject projectile = Instantiate(ctx.Weapon.Prefab, position, Quaternion.identity, container);
            return projectile.GetComponent<Projectile>();
        }

        public void Prepare(Weapon ctx)
        {
            _origin = ctx;
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Fire(Vector2 direction)
        {
            _rb.AddForce(direction.normalized * _origin.ProjectileVelocity, ForceMode2D.Impulse);
        }
    }
}