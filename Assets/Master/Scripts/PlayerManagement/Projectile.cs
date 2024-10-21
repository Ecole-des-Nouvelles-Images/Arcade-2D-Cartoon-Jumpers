using UnityEngine;

namespace Master.Scripts.PlayerManagement
{
    public class Projectile: MonoBehaviour
    {
        public AudioClip projectileHit;
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

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.CompareTag("Wall")) {
                Destroy(this.gameObject);
            } 
            else if (col.CompareTag("Enemy")) {
                ProjectileHit(col);
            }
        }

        public void ProjectileHit(Collider2D ctx)
        {
            //AudioManager.PlayClip(projectileHit);
            Destroy(this.gameObject);
        }


    }
}
