using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Scripts
{
    public class EnemyEyeController : MonoBehaviour
    {
        public PlayerController playerController;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return; // ajouter la vérification isDashing
            
            Debug.Log("Enemy Killed, reset DashCoolDown");
            playerController.ResetDashCoolDown();
            //Détruire l'ennemi 
        }
    }
}