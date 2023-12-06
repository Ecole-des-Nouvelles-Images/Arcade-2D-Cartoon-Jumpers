using UnityEngine;
using UnityEngine.InputSystem;

namespace Charlie.Scripts.Player
{
    public class PlayerController
    {
        private readonly PlayerControls _controls;
        private Rigidbody2D _rigidbody;
        private readonly Player _player;
        
        // Temporary, should be controlled by UI
        private Transform DashCursor => _player.AimingDashIndicator;
        private Transform AimCursor => _player.AimingShootIndicator;
        
        private Vector2 _aimDashDirection;
        private Vector2 _aimShootDirection;

        private float _dashRecoveryTimer = Mathf.NegativeInfinity;
        private bool CanShoot => _player.Projectile.Capacity > 0;
        
        public PlayerController(Player ctx)
        {
            _controls = new PlayerControls();
            _player = ctx;
        }

        public void ActivateInputMap()
        {
            _controls.Enable();
        }
        
        public void ListenInput()
        {
            _controls.GamePlay.Dash.performed += OnDash;
            _controls.GamePlay.AimDash.performed += OnDashAim;
            _controls.GamePlay.Shoot.performed += OnShoot;
            _controls.GamePlay.AimShoot.performed += OnAimShoot;
        }

        public void IgnoreInputs()
        {
            _controls.GamePlay.Dash.performed -= OnDash;
            _controls.GamePlay.AimDash.performed -= OnDashAim;
            _controls.GamePlay.Shoot.performed -= OnShoot;
            _controls.GamePlay.AimShoot.performed -= OnAimShoot;
        }
        
        // Event Handlers //

        public void OnDashAim(InputAction.CallbackContext ctx)
        {
            _aimDashDirection = ctx.ReadValue<Vector2>();
            float angle = Mathf.Atan2(_aimDashDirection.y, _aimDashDirection.x) * Mathf.Rad2Deg;
            DashCursor.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        
        public void OnAimShoot(InputAction.CallbackContext ctx)
        {
            _aimShootDirection = ctx.ReadValue<Vector2>();
            float shootAngle = Mathf.Atan2(_aimShootDirection.y, _aimShootDirection.x) * Mathf.Rad2Deg;
            AimCursor.rotation = Quaternion.Euler(new Vector3(0, 0, shootAngle));
        }

        public void OnDash(InputAction.CallbackContext ctx)
        {
            if (!(Time.time - _dashRecoveryTimer > _player.Dash.Cooldown)) return;

            if (_aimDashDirection == Vector2.zero) return;
            
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(_aimDashDirection * _player.Dash.Velocity, ForceMode2D.Impulse);
            _dashRecoveryTimer = Time.time;
        }

        public void OnShoot(InputAction.CallbackContext ctx)
        {
            if (!CanShoot) return;

            if (_aimShootDirection == Vector2.zero) return;
            
            _rigidbody.velocity = Vector2.zero; // conserver le reset avant le tir ? 
            _rigidbody.AddForce(-_aimShootDirection * _player.Projectile.Velocity, ForceMode2D.Impulse);
            GameObject shotFired = Object.Instantiate(_player.Projectile.Prefab, _player.transform.position, Quaternion.identity);
            Vector2 shootDirection = _aimShootDirection.normalized;
            shotFired.GetComponent<Rigidbody2D>().AddForce(shootDirection * _player.Projectile.Velocity, ForceMode2D.Impulse);
            _player.Projectile.Capacity--;
        }
        
        // Other Methods //
        
        public void ResetDashCoolDown() // Methode pour reset le cooldown du dash ( à utiliser sur le script de l'ennemi ) 
        {
            _dashRecoveryTimer = -Mathf.Infinity; 
        }
    }
}
