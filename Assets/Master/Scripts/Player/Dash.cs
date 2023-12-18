using UnityEngine;

namespace Master.Scripts.Player
{
    public class Dash
    {
        public int Power { get; private set; }
        public float Velocity { get; private set; }
        public float Cooldown { get; private set; }

        private Rigidbody2D _rb;
        
        public Dash(int power, float velocity, float cooldown)
        {
            Power = power;
            Velocity = velocity;
            Cooldown = cooldown;
        }

        public void Perform(Player ctx, Vector2 direction)
        {
            ctx.Rigidbody.velocity = Vector2.zero;
            ctx.Rigidbody.AddForce(direction * ctx.Dash.Velocity, ForceMode2D.Impulse);
        }
    }
}
