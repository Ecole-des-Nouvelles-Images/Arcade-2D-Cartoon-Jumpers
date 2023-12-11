using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Master.Scripts.Player
{
    public class PlayerController
    {
        private readonly PlayerControls _controls;
        private readonly Rigidbody2D _rigidbody;
        private readonly Player _player;

        private Transform DashCursor => _player.AimingDashIndicator; // Temporary, should be controlled by UI
        private Transform AimCursor => _player.AimingShootIndicator; // Temporary, should be controlled by UI
        private bool CanShoot => _player.Projectile.Capacity > 0;
        private bool CanDash => Time.time - _player.DashRecoveryTimer > _player.Dash.Cooldown;

        private Vector2 _aimDashDirection;
        private Vector2 _aimShootDirection;

        public Vector2 Velocity => _rigidbody.velocity;

        public PlayerController(Player ctx)
        {
            _controls = new PlayerControls();
            _player = ctx;
            _rigidbody = _player.GetComponent<Rigidbody2D>();
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

        private void OnDashAim(InputAction.CallbackContext ctx)
        {
            _aimDashDirection = ctx.ReadValue<Vector2>();
            FlipHorizontalDirection(_aimDashDirection);
            float angle = Mathf.Atan2(_aimDashDirection.y, _aimDashDirection.x) * Mathf.Rad2Deg;
            DashCursor.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private void OnAimShoot(InputAction.CallbackContext ctx)
        {
            _aimShootDirection = ctx.ReadValue<Vector2>();
            float shootAngle = Mathf.Atan2(_aimShootDirection.y, _aimShootDirection.x) * Mathf.Rad2Deg;
            AimCursor.rotation = Quaternion.Euler(new Vector3(0, 0, shootAngle));
        }

        private void OnDash(InputAction.CallbackContext ctx)
        {
            if (!CanDash) return;

            if (_aimDashDirection == Vector2.zero) return;

            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(_aimDashDirection * _player.Dash.Velocity, ForceMode2D.Impulse);
        }

        private void OnShoot(InputAction.CallbackContext ctx)
        {
            if (!CanShoot) return;

            if (_aimShootDirection == Vector2.zero) return;

            _rigidbody.velocity = Vector2.zero; // conserver le reset avant le tir ? 
            _rigidbody.AddForce(-_aimShootDirection * _player.Projectile.Velocity, ForceMode2D.Impulse);
            GameObject shotFired = Object.Instantiate(_player.Projectile.Prefab, _player.transform.position,
                Quaternion.identity);
            Vector2 shootDirection = _aimShootDirection.normalized;

            if (shotFired != null)
                shotFired.GetComponent<Rigidbody2D>()
                    .AddForce(shootDirection * _player.Projectile.Velocity, ForceMode2D.Impulse);
            else throw new Exception("Cannot Instantiate shots ammunitons");

            _player.Projectile.Capacity--;
        }

        // Other Methods //

        private void FlipHorizontalDirection(Vector2 targetDirection)
        {
            Vector3 currentDirection = _player.transform.localScale;

            if (targetDirection.x < 0 && currentDirection.x > 0 || targetDirection.x > 0 && currentDirection.x < 0)
            {
                currentDirection.x *= -1;
                _player.transform.localScale = currentDirection;
            }
        }

        public void
            ResetDashCoolDown() // Methode pour reset le cooldown du dash ( à utiliser sur le script de l'ennemi ) 
        {
            _player.DashRecoveryTimer = -Mathf.Infinity;
        }
    }
}