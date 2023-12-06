using System;
using System.Collections.Generic;
using Alex.Scripts.Player;
using UnityEngine;

namespace Alex.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        
        public List<Command> Commands;
        public float EnemySpeed = 5f;
        public float EnemyPower;
        //public abstract void Move(IMoveCommand moveCommand);
        public PlayerController Player;
        
        private int _currentCommandIndex;
        private Command _currentCommand;
        
        private void Update() {
            // Control to check if commands list is empty
            if (Commands.Count == 0)
                throw new Exception("Enemy " + name + " commands is empty");
            // If no command then fetch first command
            if (!_currentCommand) {
                _currentCommand = Commands[0];
            }
            // If current command is finished then get the next
            if (_currentCommand.IsFinished(this)) {
                _currentCommandIndex++;
                // If index is higher than the number of elements
                if (_currentCommandIndex >= Commands.Count) 
                    _currentCommandIndex = 0;
                // Fetch the next command
                _currentCommand = Commands[_currentCommandIndex];
            }
            _currentCommand.Execute(this);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return; // ajouter la vérification isDashing
            
            Debug.Log("Enemy Killed, reset DashCoolDown");
            Player.ResetDashCoolDown();
            //Détruire l'ennemi 
        }
    }
}