using Charlie.Scripts.SO;
using UnityEngine;

namespace Charlie.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        // Public inspector fields //
        
        [Header("Base Stats")]
        [Range(0, 1000)] public int MaxHp;

        [Header("Power Components")]
        public DashSO StartingDashType;
        public ProjectileSO StartingProjectileType;
        
        [Space(5)] [Header("Temporary indicators, should move to UI")]
        [Tooltip("Move to UI !!!")] public Transform AimingDashIndicator;
        [Tooltip("Move to UI !!!")] public Transform AimingShootIndicator;
        
        private void OnValidate()
        {
            MaxHp = Mathf.RoundToInt(MaxHp / 10f) * 10;
        }

        // Important fields and properties //
        
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
        public Dash Dash 
        {
            get; private set;
        }
        public Projectile Projectile {
            get; private set;
        }
        
        // Minor permanent fields //
        
        // private float _dashRecoveryTimer;
        private float _projectileRecoveryTimer;
        
        // ======================== //
        
        private void Awake()
        {
            _controller = new PlayerController(this);
            CurrentDashType = StartingDashType;
            CurrentProjectileType = StartingProjectileType;
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
        }
    }
}
