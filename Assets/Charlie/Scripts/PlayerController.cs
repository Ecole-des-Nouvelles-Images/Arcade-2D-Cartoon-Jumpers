using UnityEngine;
using UnityEngine.InputSystem;

namespace Charlie.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float DashForce = 10f;

        public bool IsOnSurface => _surfaceHitbox.IsOnSurface;
        
        private InputAction _onDash;
        private InputAction _onAim;

        private Rigidbody2D _rigidbody;
        private SurfaceHitbox _surfaceHitbox;
        private Vector2 _aimDirection;

        private void Awake()
        {
            _onDash = new InputAction("Gameplay/Dash"); 
            _onAim = new InputAction("Gameplay/Aim");
        }
        

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _surfaceHitbox = GetComponent<SurfaceHitbox>();
        }
        
        private void OnEnable()
        {
            _onDash.Enable();
            _onAim.Enable();
            _onDash.performed += OnDash;
            _onAim.performed += OnAim;
        }
        
        private void OnDisable()
        {
            _onDash.Disable();
            _onAim.Disable();
            _onDash.performed -= OnDash;
            _onAim.performed -= OnAim;
        }
    
        // Actions Handlers
    
        public void OnAim(InputAction.CallbackContext ctx)
        {
            _aimDirection = ctx.ReadValue<Vector2>();
        }
    
        public void OnDash(InputAction.CallbackContext ctx)
        {
            if (_aimDirection == Vector2.zero) return;
            
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(_aimDirection * DashForce, ForceMode2D.Impulse);
        }
    }
}
