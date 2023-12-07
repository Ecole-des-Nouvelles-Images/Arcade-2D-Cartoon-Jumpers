using System;
using Charlie.Scripts.SO;
using UnityEngine;

namespace Charlie.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        // Public inspector fields //
        
        [Header("Base Stats")]
        [Range(0, 1000)] [SerializeField] private int _maxHp;

        [Header("Power Components")]
        [SerializeField] private DashSO _startingDashType;
        [SerializeField] private ProjectileSO _startingProjectileType;
        
        [Space(5)] [Header("Temporary indicators, should move to UI")]
        [Tooltip("Move to UI !!!")] public Transform AimingDashIndicator;
        [Tooltip("Move to UI !!!")] public Transform AimingShootIndicator;

        [Space(5)]
        [Range(0, 200)] [SerializeField] private float _velocityThreshold;
        
        private void OnValidate()
        {
            _maxHp = Mathf.RoundToInt(_maxHp / 10f) * 10;
        }

        // Important fields and properties //

        public static Action<int> OnDirectionChange;
        
        private PlayerController _controller;
        
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
        
        // Minor permanent fields //
        
        // private float _dashRecoveryTimer;
        private float _projectileRecoveryTimer;
        private float _previousVelocity;

        private void OnGUI()
        {
            GUI.Label(new Rect(0, 30, 100, 100), "CurrentVelocity: " + _controller.Velocity);
        }

        // ======================== //
        
        private void Awake()
        {
            _controller = new PlayerController(this);
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
            CheckClimbAscension();
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
        
        private void CheckClimbAscension()
        {
            float currentVelocity = _controller.Velocity.y;

            if (currentVelocity < -_velocityThreshold && _previousVelocity >= -_velocityThreshold) {
            
                OnDirectionChange.Invoke(-1);
            }
            else if ((currentVelocity >= -_velocityThreshold && currentVelocity <= _velocityThreshold) && 
                     (_previousVelocity < -_velocityThreshold || _previousVelocity > _velocityThreshold)) {
                OnDirectionChange.Invoke(0);
            }
            else if (currentVelocity > _velocityThreshold && _previousVelocity <= _velocityThreshold) {
                OnDirectionChange.Invoke(1);
            }
            
            _previousVelocity = currentVelocity;
        }
    }
}
