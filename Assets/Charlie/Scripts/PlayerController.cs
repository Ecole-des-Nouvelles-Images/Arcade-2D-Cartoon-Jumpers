using UnityEngine;
using UnityEngine.InputSystem;

namespace Charlie.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float DashForce = 10f;
        public float DashCooldown = 2f;
        
        [Space(10)]
        public Transform AimingIndicator;

        private PlayerInputActions _inputActions;
        private Rigidbody2D _rigidbody;
        
        private Vector2 _aimDirection;
        private float _lastDashTime = Mathf.NegativeInfinity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _inputActions = new PlayerInputActions();
        }

        private void Start()
        {
            _inputActions.Player.Enable();
        }
        
        private void OnEnable()
        {
            _inputActions.Player.Dash.performed += OnDash;
            _inputActions.Player.Aim.performed += OnAim;
        }

        private void OnDisable()
        {
            _inputActions.Player.Dash.performed -= OnDash;
            _inputActions.Player.Aim.performed -= OnAim;
        }
        
        // Event Handlers

        public void OnAim(InputAction.CallbackContext ctx)
        {
            _aimDirection = ctx.ReadValue<Vector2>();
            float angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
            AimingIndicator.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        public void OnDash(InputAction.CallbackContext ctx)
        {
            if (!(Time.time - _lastDashTime > DashCooldown)) return;

            if (_aimDirection == Vector2.zero) return;
            
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(_aimDirection * DashForce, ForceMode2D.Impulse);
            _lastDashTime = Time.time;
        }
    }
}
