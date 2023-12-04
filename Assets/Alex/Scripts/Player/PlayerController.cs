using UnityEngine;
using UnityEngine.InputSystem;

namespace Alex.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float DashForce = 50f;
        public float DashCooldown = 1f;
        public float ShootForce = -10f;
        public float ShootCooldown = 1f;
        
        [Space(10)]
        public Transform AimingDashIndicator;
        public Transform AimingShootIndicator;

        private PlayerInputActions _inputActions;
        
        private Rigidbody2D _rigidbody;
        private Vector2 _aimDashDirection;
        private Vector2 _aimShootDirection;

        private float _lastDashTime = Mathf.NegativeInfinity;
        private float _lastShotTime = Mathf.NegativeInfinity;

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
        }

        private void Start()
        {
            _inputActions.Enable();
            _rigidbody = GetComponent<Rigidbody2D>();

        }
        
        private void OnEnable()
        {
            
            _inputActions.GamePlay.Dash.performed += OnDash;
            _inputActions.GamePlay.AimDash.performed += OnDashAim;
            _inputActions.GamePlay.Shoot.performed += OnShoot;
            _inputActions.GamePlay.AimShoot.performed += OnAimShoot;

        }

        private void OnDisable()
        {
            _inputActions.GamePlay.Dash.performed -= OnDash;
            _inputActions.GamePlay.AimDash.performed -= OnDashAim;
            _inputActions.GamePlay.Shoot.performed -= OnShoot;
            _inputActions.GamePlay.AimShoot.performed -= OnAimShoot;
        }
        
        // Event Handlers

        public void OnDashAim(InputAction.CallbackContext ctx)
        {
            _aimDashDirection = ctx.ReadValue<Vector2>();
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
                    _rigidbody.AddForce(_aimShootDirection! * ShootForce, ForceMode2D.Impulse);
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
