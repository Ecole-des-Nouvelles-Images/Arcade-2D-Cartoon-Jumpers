namespace Charlie.Scripts.Player
{
    public class Dash
    {
        public int Power { get; private set; }
        public float Velocity { get; private set; }
        public float Cooldown { get; private set; }
        
        public Dash(int power, float velocity, float cooldown)
        {
            Power = power; Velocity = velocity; Cooldown = cooldown;
        }
    }
}