using UnityEngine;
using UnityEngine.InputSystem;

namespace Master.Scripts.Player
{
    public class PlayerControllerOld : MonoBehaviour
    {
        public float DashForce = 80f;
        public float DashCooldown = 1f;
        public float ShootForce = -40f;
        public float ShootCooldown = 1f;
        
        [Space(10)]
        public Transform AimingDashIndicator;
        public Transform AimingShootIndicator;
        [Space] 
        public GameObject Projectile;

        public int AvailableShots = 3;

        public float ProjectileVelocity = 80f;

        private PlayerControls _controls;
        
        private Rigidbody2D _rigidbody;
        private Vector2 _aimDashDirection;
        private Vector2 _aimShootDirection;

        private float _lastDashTime = Mathf.NegativeInfinity;
        private float _lastShotTime = Mathf.NegativeInfinity;

        private void Awake()
        {
            _controls = new PlayerControls();
        }

        private void Start()
        {
            _controls.Enable();
            _rigidbody = GetComponent<Rigidbody2D>();

        }
        
        private void OnEnable()
        {
            
            _controls.GamePlay.Dash.performed += OnDash;
            _controls.GamePlay.AimDash.performed += OnDashAim;
            _controls.GamePlay.Shoot.performed += OnShoot;
            _controls.GamePlay.AimShoot.performed += OnAimShoot;

        }

        private void OnDisable()
        {
            _controls.GamePlay.Dash.performed -= OnDash;
            _controls.GamePlay.AimDash.performed -= OnDashAim;
            _controls.GamePlay.Shoot.performed -= OnShoot;
            _controls.GamePlay.AimShoot.performed -= OnAimShoot;
        }

        private void Update()
        {
            if (Time.time - _lastShotTime >= 1f && AvailableShots < 3)
            {
                AvailableShots++;
                _lastShotTime = Time.time;
            }
        }
        // Event Handlers

        public void OnDashAim(InputAction.CallbackContext ctx)
        {
            _aimDashDirection = ctx.ReadValue<Vector2>();
            if (ctx.ReadValue<Vector2>().x < 0) transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f); //Ajout
            if (ctx.ReadValue<Vector2>().x > 0) transform.localScale = new Vector3(0.6f, 0.6f, 0.6f); //Ajout
            float angle = Mathf.Atan2(_aimDashDirection.y, _aimDashDirection.x) * Mathf.Rad2Deg;
            AimingDashIndicator.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        
        public void OnAimShoot(InputAction.CallbackContext ctx)
        {
            _aimShootDirection = ctx.ReadValue<Vector2>();
            float shootAngle = Mathf.Atan2(_aimShootDirection.y, _aimShootDirection.x) * Mathf.Rad2Deg;
            AimingShootIndicator.rotation = Quaternion.Euler(new Vector3(0, 0, shootAngle));
        }

        public void OnDash(InputAction.CallbackContext ctx)
        {
            if (!(Time.time - _lastDashTime > DashCooldown)) return;

            if (_aimDashDirection == Vector2.zero) return;
            
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(_aimDashDirection * DashForce, ForceMode2D.Impulse);
            _lastDashTime = Time.time;
        }

        public void OnShoot(InputAction.CallbackContext ctx)
        {
            if (Time.time - _lastShotTime > ShootCooldown)
            {
                if (_aimShootDirection != Vector2.zero)
                {
                    Debug.Log("Shot");
                    _rigidbody.velocity = Vector2.zero; // conserver le reset avant le tir ? 
                    _rigidbody.AddForce(-_aimShootDirection * ShootForce, ForceMode2D.Impulse);
                    GameObject shotFired = Instantiate(Projectile, transform.position, Quaternion.identity);
                    Vector2 shootDirection = _aimShootDirection.normalized;
                    shotFired.GetComponent<Rigidbody2D>().AddForce( shootDirection * ProjectileVelocity, ForceMode2D.Impulse);
                    AvailableShots--;
                    _lastShotTime = Time.time;

                }
            }
        }
        public void ResetDashCoolDown() // Methode pour reset le cooldown du dash ( à utiliser sur le script de l'ennemi ) 
        {
            _lastDashTime = -Mathf.Infinity; 
        }
    }
}
