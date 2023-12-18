using System;
using System.ComponentModel;
using UnityEngine;

using Master.Scripts.Camera;
using Master.Scripts.Common;
using Master.Scripts.Managers;
using Master.Scripts.SO;
using EnemyComponent = Master.Scripts.Enemy.Enemy;

namespace Master.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        // Public inspector fields //
        
        [SerializeField] private bool _enableTestMapInputs;
        
        [Header("Base Stats")]
        [Range(0, 1000)] [SerializeField] private int _initialMaxHealth = 100;
        [Range(0, 100)] [SerializeField] private int _woundedThreshold;

        [Header("Power Components")]
        [SerializeField] private DashSO _startingDashType;
        [SerializeField] private WeaponSO _startingWeaponType;

        [Header("Score System")]
        [SerializeField] private float _scoreDenominator = 10f;
        
        [Space(5)] [Header("Temporary indicators, should move to UI")]
        [Tooltip("Move to UI !!!")] public Transform AimingDashIndicator;
        [Tooltip("Move to UI !!!")] public Transform AimingShootIndicator;
        
        private void OnValidate()
        {
            _initialMaxHealth = Mathf.RoundToInt(_initialMaxHealth / 10f) * 10;
            if (_scoreDenominator == 0) {
                _scoreDenominator = 1;
                throw new DivideByZeroException("Trying to set Score Denominator to 0. Fallback to 1.");
            }
        }

        // Events //

        public static Action<int> OnDirectionChange;
        public static Action<Player> OnHealthChanged;
        public static Action<Player> OnScoreChanged;
        
        // Important fields and properties //
        
        private PlayerController _controller;
        public Animator Animator { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }

        private DashSO _currentDashType;
        private WeaponSO _currentWeaponType;

        public DashSO CurrentDashType {
            set {
                _currentDashType = value;
                Dash = _currentDashType.ConstructDashObject();
            }
        }
        public WeaponSO CurrentWeaponType {
            set {
                _currentWeaponType = value;
                Weapon = _currentWeaponType.ConstructWeaponObject();
            }
        }
        public Dash Dash { get; private set; }
        public Weapon Weapon { get; private set; }
        
        // Other fields //
        
        public int MaxHealth { get; private set; }
        public int Health { get; set; }
        
        public float Score { get; private set; }
        
        private static float VelocityThreshold => CameraController.VelocityThreshold;
        private static readonly int AnimationSpeed = Animator.StringToHash("Speed");
        
        public float DashRecoveryTimer { get; private set; }
        public bool IsDashing => Animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerDash");

        private float _projectileRecoveryTimer;
        private float _previousVelocity;

        // ======================== //
        
        private void Awake()
        {
            _controller = new PlayerController(this, _enableTestMapInputs);
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody2D>();
            
            CurrentDashType = _startingDashType;
            CurrentWeaponType = _startingWeaponType;

            MaxHealth = _initialMaxHealth;
            Health = MaxHealth;
        }

        private void Start()
        {
            _controller.ActivateInputMap();
            // _dashRecoveryTimer = Dash.Cooldown;
            _projectileRecoveryTimer = Weapon.Cooldown;
        }

        private void OnEnable()
        {
            _controller.ListenInput();
            EnemyComponent.OnAwake = OnEnemyAwakening;
        }
        
        private void OnDisable()
        {
            _controller.IgnoreInputs();
            EnemyComponent.OnAwake = OnEnemyAwakening;
        }

        private void Update()
        {
            UpdateVerticalDirection();
            UpdateAnimation();
            RecoverShots();
            UpdateScore();
        }

        // ======================== //
        
        private void RecoverShots()
        {
            if (Weapon.Capacity == Weapon.MaxCapacity) return;

            if (_projectileRecoveryTimer <= 0f)
            {
                Weapon.Capacity++;
                _projectileRecoveryTimer = 0f;
            }

            _projectileRecoveryTimer -= Time.deltaTime;
        }

        public void ResetDash()
        {
            DashRecoveryTimer = -Mathf.Infinity;
        }
        
        // New enemy event subscription //
        
        private void OnEnemyAwakening(EnemyComponent enemy)
        {
            enemy.OnHit += DealDamage;
            enemy.OnAttack += TakeDamage;
            enemy.OnKill += OnEnemyKill;
        }

        private void OnEnemyKill(EnemyComponent enemy)
        {
            enemy.OnHit -= DealDamage;
            enemy.OnAttack -= TakeDamage;
            enemy.OnKill -= OnEnemyKill;
            
            Destroy(enemy.gameObject);
        }
        
        // Events Handlers //

        private void DealDamage(DmgType type, EnemyComponent enemy)
        {
            switch (type)
            {
                case DmgType.Dash:
                    enemy.Health -= Dash.Power;
                    ResetDash();
                    break;
                case DmgType.Projectile:
                    enemy.Health -= Weapon.Power;
                    break;
                default:
                    throw new InvalidEnumArgumentException($"Unimplemented {type.ToString()} damage type");
            }
            
            if (enemy.Health <= 0)
                enemy.OnKill.Invoke(enemy);
        }

        private void TakeDamage(int damages)
        {
            Health -= damages;
            OnHealthChanged.Invoke(this);
            
            if (Health <= 0) {
                Debug.Log("Game Over");
                SceneLoader.Instance.LoadNextScene();
            }
            else if (Health / MaxHealth < _woundedThreshold)
            {
                Debug.Log("Player Wounded"); // TODO
            }
        }
        
        // Methods

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
            Animator.SetFloat(AnimationSpeed, Rigidbody.velocity.y);
        }
        
        private void UpdateScore()
        {
            float currentHeight = transform.position.y / _scoreDenominator;

            if (currentHeight < Score) return;
            
            Score = currentHeight;
            OnScoreChanged.Invoke(this);
        }
    }
}
