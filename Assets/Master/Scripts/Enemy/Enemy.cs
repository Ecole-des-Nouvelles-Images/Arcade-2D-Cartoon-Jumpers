using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

using Master.Scripts.Common;
using Master.Scripts.SO;
using PlayerComponent = Master.Scripts.PlayerManagement.Player;
using Random = UnityEngine.Random;

namespace Master.Scripts.Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        public static Action<Enemy> OnAwake;

        [Header("Sounds")] 
        public AudioClip[] AttackSound;

        public AudioClip IdleSound;
        
        [Header("Pattern")]
        [SerializeField] private List<CommandSO> _commands;

        [Header("Statistics")]
        [SerializeField] private float _initialMaxHP;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private int _power;

        // Properties
        public SpriteRenderer SpriteRenderer { get; private set; }
        public PlayerComponent PlayerReference { get; private set; }
        
        public bool HasCollidedWithPlayer { get; private set; }

        public float MaxHealth { get; private set; }
        public float Health { get; set; }
        public float Speed => _speed;
        public int Power => _power;

        private Color _initialColor;
        
        // Individual events

        public Action<DmgType, Enemy> OnHit;
        public Action<int> OnAttack;
        public Action<Enemy> OnKill;
        
        // Other fields
        
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        public Dictionary<(CommandSO, string), object> Memory = new();
        private CommandSO _currentCommand;
        private int _currentCommandIndex;
        
        // Animator
        public Animator Animator;
        // AudioSource
        public AudioSource AudioSource;

        private static readonly int AttackTrigger = Animator.StringToHash("AttackTrigger");
        // Methods
        
        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            PlayerReference = GameObject.Find("Player").GetComponent<PlayerComponent>();
            MaxHealth = _initialMaxHP;
            Health = MaxHealth;
            Animator = GetComponent<Animator>();
            AudioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            OnAwake.Invoke(this);
            _initialColor = SpriteRenderer.color;
        }

        private void Update() {
            // Control to check if commands list is empty
            if (_commands.Count == 0)
                throw new WarningException("Enemy " + name + " commands is empty");
            // If no command then fetch first command
            if (!_currentCommand) {
                _currentCommand = _commands[0];
                _currentCommand.Setup(this);
            }
            // If current command is finished then get the next
            if (_currentCommand.IsFinished(this)) {
                _currentCommand.CleanUp(this);
                _currentCommandIndex++;
                // If index is higher than the number of elements
                if (_currentCommandIndex >= _commands.Count) 
                    _currentCommandIndex = 0;
                // Fetch the next command
                _currentCommand = _commands[_currentCommandIndex];
                _currentCommand.Setup(this);
            }
            _currentCommand.Execute(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "Player":
                    HandlePlayerState(other.gameObject);
                    break;
                
                case "Projectile":
                    OnHit.Invoke(DmgType.Projectile, this);
                    StartCoroutine(ProjectileFeedbackCoroutine(new Color(236, 94, 33, 255), 1));
                    break;
            }
        }
        
        // Utils //
        
        private IEnumerator ProjectileFeedbackCoroutine(Color targetColor, float duration)
        {
            SpriteRenderer.color = targetColor;

            yield return new WaitForSeconds(duration);

            SpriteRenderer.color = _initialColor;
        }

        private void HandlePlayerState(GameObject ctx)
        {
            PlayerComponent player = ctx.GetComponent<PlayerComponent>();

            if (player.IsDashing) {
                OnHit.Invoke(DmgType.Dash, this);
            }
            else {
              
                Animator.SetTrigger(AttackTrigger);
                OnAttack.Invoke(this.Power);
                if (AttackSound.Length > 0)
                {
                    int randomIndex = Random.Range(0, AttackSound.Length);
                    AudioSource.clip = AttackSound[randomIndex];
                    AudioSource.Play();
                }
            }
        }
        public void ResetShooting()
        {
            Animator.SetBool("IsShooting", false);
        }
    }
}
