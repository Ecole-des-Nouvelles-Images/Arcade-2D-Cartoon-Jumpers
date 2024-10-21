using Master.Scripts.Managers;
using Master.Scripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Master.Scripts.PlayerManagement
{
    public class PlayerController
    {
        public readonly PlayerControls Controls;
        
        private readonly Player _player;
        private readonly bool _enableTestMap;

        private Transform DashCursor => UIManager.Instance.DashCursor; // Temporary, should be controlled by UI
        private Transform AimCursor => UIManager.Instance.AimLine; // Temporary, should be controlled by UI
        private bool CanShoot => _player.Weapon.Capacity > 0;
        private bool CanDash => Time.time - _player.DashRecoveryTimer > _player.Dash.Cooldown;

        private Vector2 _aimDashDirection;
        private Vector2 _aimShootDirection;

        public Vector2 Velocity => _player.Rigidbody.velocity;

        public PlayerController(Player ctx, bool enableTestMap)
        {
            Controls = new PlayerControls();
            _enableTestMap = enableTestMap;
            _player = ctx;
        }

        public void ActivateInputMap()
        {
            Controls.GamePlay.Enable();
            
            if (_enableTestMap)
                Controls.TestMap.Enable();
        }

        public void ListenInput()
        {
            Controls.GamePlay.Dash.performed += OnDash;
            Controls.GamePlay.AimDash.performed += OnDashAim;
            Controls.GamePlay.Shoot.performed += OnShoot;
            Controls.GamePlay.AimShoot.performed += OnAimShoot;
            Controls.GamePlay.Pause.performed += OnPause;
            
            if (_enableTestMap) {
                Controls.TestMap.NextScene.performed += OnNextScene;
                Controls.TestMap.PrevScene.performed += OnPrevScene;
            }
        }

        public void IgnoreInputs()
        {
            Controls.GamePlay.Dash.performed -= OnDash;
            Controls.GamePlay.AimDash.performed -= OnDashAim;
            Controls.GamePlay.Shoot.performed -= OnShoot;
            Controls.GamePlay.AimShoot.performed -= OnAimShoot;
            
            if (_enableTestMap) {
                Controls.TestMap.NextScene.performed += OnNextScene;
                Controls.TestMap.PrevScene.performed += OnPrevScene;
            }
        }

        // Event Handlers //

        private void OnDashAim(InputAction.CallbackContext ctx)
        {
            _aimDashDirection = ctx.ReadValue<Vector2>();
            FlipHorizontalDirection(_aimDashDirection);
            float angle = Mathf.Atan2(-_aimDashDirection.y, -_aimDashDirection.x) * Mathf.Rad2Deg;
            DashCursor.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private void OnAimShoot(InputAction.CallbackContext ctx)
        {
            _aimShootDirection = ctx.ReadValue<Vector2>();
            float shootAngle = Mathf.Atan2(-_aimShootDirection.y, -_aimShootDirection.x) * Mathf.Rad2Deg;
            AimCursor.localRotation = Quaternion.Euler(new Vector3(0, 0, shootAngle));
        }

        private void OnDash(InputAction.CallbackContext ctx)
        {
            if (!CanDash) return;

            if (_aimDashDirection == Vector2.zero) return;

            _player.Dash.Perform(_player, _aimDashDirection);
        }

        private void OnShoot(InputAction.CallbackContext ctx) // TODO: Debug cooldown/capacity
        {
            if (!CanShoot) return;

            if (_aimShootDirection == Vector2.zero) return;

            _player.Weapon.Shoot(_player, _aimShootDirection);
        }

        void OnPause(InputAction.CallbackContext ctx)
        {
            Pause();
        }

        public void Pause()
        {
            GameManager.Instance.SetPause();
            if (GameManager.Instance.IsPaused)
            {
                Controls.UIMenu.Enable();
                Controls.GamePlay.Disable();
            }
            else
            {
                Controls.GamePlay.Enable();
                Controls.UIMenu.Disable();
            }
            
        }

        // Test Input Event Handlers //
        
        private void OnPrevScene(InputAction.CallbackContext ctx)
        {
            SceneLoader.Instance.LoadPreviousScene();
        }

        private void OnNextScene(InputAction.CallbackContext ctx)
        {
            SceneLoader.Instance.LoadNextScene();
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
    }
}