using System;
using System.Collections.Generic;
using Alex.Scripts.Enemies;
using Alex.Scripts.Player;
using UnityEngine;

namespace Alex.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public List<MoveCommand> moveCommands;
        public float EnemySpeed = 5f;
        public float EnemyPower;
        //public abstract void Move(IMoveCommand moveCommand);
        public PlayerController Player;

        private int currentMoveIndex = 0;
        private bool isMoving = false;

        private void Update()
        {
            if (!isMoving && currentMoveIndex < moveCommands.Count)
            {
                Move(moveCommands[currentMoveIndex]);
                // !! Ajouter logique de fin
                currentMoveIndex++;
            }
        }
        public void Move(IMoveCommand moveCommand)
        {
            if (moveCommand != null)
            {
                isMoving = true;
                Vector2 _destination = moveCommand.Execute(transform); // récupère la destination de l'instance du SO
                Vector2 _direction = (_destination - (Vector2)transform.position);
                transform.Translate(_direction * (EnemySpeed * Time.deltaTime));
            }
           
        }

       /* private void Start()
        {
            foreach (MoveCommand moveCommand in moveCommands)
            {
                Move(moveCommand);
            }
        }
*/
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return; // ajouter la vérification isDashing
            
            Debug.Log("Enemy Killed, reset DashCoolDown");
            Player.ResetDashCoolDown();
            //Détruire l'ennemi 
        }
    }
}