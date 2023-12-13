using System;
using UnityEngine;

using Master.Scripts.Camera;
using Master.Scripts.SO;

namespace Master.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        // Public inspector fields //
        
        [SerializeField] private bool _enableTestMapInputs;
        
        [Header("Base Stats")]
        [Range(0, 1000)] [SerializeField] private int _maxHp = 100;

        [Header("Power Components")]
        [SerializeField] private DashSO _startingDashType;
        [SerializeField] private ProjectileSO _startingProjectileType;
        
        [Space(5)] [Header("Temporary indicators, should move to UI")]
        [Tooltip("Move to UI !!!")] public Transform AimingDashIndicator;
        [Tooltip("Move to UI !!!")] public Transform AimingShootIndicator;
        
        private void OnValidate()
        {
            _maxHp = Mathf.RoundToInt(_maxHp / 10f) * 10;
        }

        // Events //

        public static Action<int> OnDirectionChange;
        public static Action<Player> OnEnemyHit;
        public static Action<Player> OnDamageTaken;
        public static Action<Player> OnHealthChanged;
        
        // Important fields and properties //
        
        private PlayerController _controller;
        private Animator _animator;
        
        private DashSO _currentDashType;
        private ProjectileSO _currentProjectileType;

        public DashSO CurrentDashType {
            set {
                _currentDashType = value;
                Dash = _currentDashType.ConstructDashObject();
            }
        }
        public ProjectileSO CurrentProjectileType {
            set {
                _currentProjectileType = value;
                Projectile = _currentProjectileType.ConstructProjectileObject();
            }
        }
        public Dash Dash { get; private set; }
        public Projectile Projectile { get; private set; }
        
        // Other fields //
        
        public int HealthPoint { get; set; }
        
        private static float VelocityThreshold => CameraController.VelocityThreshold;
        private static readonly int AnimationSpeed = Animator.StringToHash("Speed");
        
        public float DashRecoveryTimer { get; set; }
        private bool IsDashing => _animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerDash");

        private float _projectileRecoveryTimer;
        private float _previousVelocity;

        private void OnGUI()
        {
            GUI.Label(new Rect(0, 30, 100, 100), "CurrentVelocity: " + _controller.Velocity);
        }

        // ======================== //
        
        private void Awake()
        {
            _controller = new PlayerController(this, _enableTestMapInputs);
            _animator = GetComponent<Animator>();
            CurrentDashType = _startingDashType;
            CurrentProjectileType = _startingProjectileType;
            HealthPoint = _maxHp;
        }

        private void Start()
        {
            _controller.ActivateInputMap();
            // _dashRecoveryTimer = Dash.Cooldown;
            _projectileRecoveryTimer = Projectile.Cooldown;
        }

        private void OnEnable()
        {
            _controller.ListenInput();
        }

        private void OnDisable()
        {
            _controller.IgnoreInputs();
        }

        private void Update()
        {
            UpdateVerticalDirection();
            UpdateAnimation();
            RecoverShots();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (IsDashing)
                    OnEnemyHit.Invoke(this);
                else
                {
                    OnDamageTaken.Invoke(this);
                    OnHealthChanged.Invoke(this); // TODO
                }
            }
        }

        // ======================== //
        
        private void RecoverShots()
        {
            if (Projectile.Capacity == Projectile.MaxCapacity) return;

            if (_projectileRecoveryTimer <= 0f)
            {
                Projectile.Capacity++;
                _projectileRecoveryTimer = 0f;
            }

            _projectileRecoveryTimer -= Time.deltaTime;
        }

        public void ResetDash()
        {
            DashRecoveryTimer = -Mathf.Infinity;
        }
        
        private void UpdateVerticalDirection() 
        {
            float currentVelocity = _controller.Velocity.y;

            if (OnDirectionChange == null) return;

            if (currentVelocity < -VelocityThreshold && _previousVelocity >= -VelocityThreshold) {
                OnDirectionChange.Invoke(-1);
            }
            else if ((currentVelocity >= -VelocityThreshold && currentVelocity <= VelocityThreshold) && 
                     (_previousVelocity < -VelocityThreshold || _previousVelocity > VelocityThreshold)) {
                OnDirectionChange.Invoke(0);
            }
            else if (currentVelocity > VelocityThreshold && _previousVelocity <= VelocityThreshold) {
                OnDirectionChange.Invoke(1);
            }
            
            _previousVelocity = currentVelocity;
        }

        private void UpdateAnimation()
        {
            _animator.SetFloat(AnimationSpeed, _controller.Velocity.y);
        }
    }
}
