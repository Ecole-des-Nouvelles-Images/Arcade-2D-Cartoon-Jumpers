using System;
using System.Collections.Generic;
using Master.Scripts.Player;
using UnityEngine;

using Master.Scripts.SO;
using PlayerComponent = Master.Scripts.Player.Player;

namespace Master.Scripts.Enemy
{
    public abstract class Enemy : MonoBehaviour {

        public Dictionary<(CommandSO, string), object> Memory = new();
        
        [Header("Pattern")]
        public List<CommandSO> Commands;
        
        [Header("Statistics")]
        public float EnemySpeed = 5f;
        public int EnemyPower;
        public float MaxHP;
        
        public PlayerComponent PlayerReference { get; private set; }

        private CommandSO _currentCommand;
        private int _currentCommandIndex;
        public bool HasCollidedWithPlayer { get; private set; }

        private float HealthPoint { get; set; }

        private void Awake()
        {
            PlayerReference = GameObject.Find("Player").GetComponent<PlayerComponent>();
            HealthPoint = MaxHP;
        }

        private void OnEnable()
        {
            PlayerComponent.OnEnemyHit += OnHit;
            PlayerComponent.OnDamageTaken += DealDamage;
        }

        private void OnDisable()
        {
            PlayerComponent.OnEnemyHit -= OnHit;
            PlayerComponent.OnDamageTaken -= DealDamage;
        }

        private void Update() {
            // Control to check if commands list is empty
            if (Commands.Count == 0)
                throw new Exception("Enemy " + name + " commands is empty");
            // If no command then fetch first command
            if (!_currentCommand) {
                _currentCommand = Commands[0];
                _currentCommand.Setup(this);
            }
            // If current command is finished then get the next
            if (_currentCommand.IsFinished()) {
                _currentCommand.CleanUp();
                _currentCommandIndex++;
                // If index is higher than the number of elements
                if (_currentCommandIndex >= Commands.Count) 
                    _currentCommandIndex = 0;
                // Fetch the next command
                _currentCommand = Commands[_currentCommandIndex];
                _currentCommand.Setup(this);
            }
            _currentCommand.Execute();
        }

        private void OnHit(PlayerComponent ctx) // TODO: Test
        {
            HealthPoint -= ctx.Dash.Power;
            ctx.ResetDash();
        }

        private void DealDamage(PlayerComponent ctx) // TODO: Test
        {
            ctx.HealthPoint -= EnemyPower;
        }
    }
}
