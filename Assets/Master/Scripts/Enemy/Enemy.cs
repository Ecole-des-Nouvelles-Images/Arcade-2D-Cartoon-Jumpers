using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

using Master.Scripts.Common;
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
            PlayerComponent.OnEnemyHit += OnHit; // TODO : Event common to every ennemies. Revert logic towards player ?
            PlayerComponent.OnDamageTaken += DealDamage;
        }

        private void OnDisable()
        {
            PlayerComponent.OnEnemyHit -= OnHit; // TODO : Event common to every ennemies. Revert logic towards player ?
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

        // Events Handlers //
        
        private void OnHit(PlayerComponent ctx, DmgType type) // TODO: Test
        {
            Debug.Log($"Enemy {this.gameObject.name} took damages");

            if (type == DmgType.Dash) {
                HealthPoint -= ctx.Dash.Power;
                ctx.ResetDash();
            }
            else if (type == DmgType.Projectile) {
                HealthPoint -= ctx.Weapon.Power;
            }
            else {
                throw new InvalidEnumArgumentException($"Unimplemented {type.ToString()} damage type");
            }
            
            if (HealthPoint <= 0)
                Destroy(this.gameObject);
        }

        private void DealDamage(PlayerComponent ctx)
        {
            ctx.HealthPoint -= EnemyPower;
        }
    }
}
