using UnityEngine;

using Master.Scripts.Common;

namespace Master.Scripts.Player
{
    public class Projectile : MonoBehaviour
    {
        private Weapon _origin;
        private Rigidbody2D _rb;

        private static Player _playerReference;

        public static Projectile Create(Player ctx, Vector3 position, Transform container)
        {
            _playerReference = ctx;
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Scripts.Player.Player.OnEnemyHit.Invoke(_playerReference, DmgType.Projectile);
                Destroy(this.gameObject);
            }
        }
    }
}