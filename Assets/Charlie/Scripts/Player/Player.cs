using System;
using UnityEngine;
using NaughtyAttributes;

// using Master.Scripts.Camera;
using Master.Scripts.SO;
using Dash = Master.Scripts.Player.Dash;
using Projectile = Master.Scripts.Player.Projectile;
using Charlie.Scripts.Camera;

namespace Charlie.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        // Public inspector fields //
        
        [SerializeField] private bool _enableTestMapInputs;
        
        [Header("Base Stats")]
        [Range(0, 1000)] [SerializeField] private int _maxHp;

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

        // Important fields and properties //

        public static Action<int> OnDirectionChange;
        
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

        private float VelocityThreshold => CameraController.DeadzoneThreshold;
        
        // Animation cached parameters //
        
        private static readonly int AnimationSpeed = Animator.StringToHash("Speed");
        
        // Minor permanent fields //
        
        public float DashRecoveryTimer { get; set; }
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
        
        private void UpdateVerticalDirection()
        {
            float currentVelocity = _controller.Velocity.y;

            if (OnDirectionChange == null)
                return;

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
