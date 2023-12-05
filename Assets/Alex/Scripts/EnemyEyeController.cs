using UnityEngine;

namespace Alex.Scripts
{
    public class EnemyEyeController : MonoBehaviour
    {

        public Player.PlayerController Player;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return; // ajouter la vérification isDashing
            
            Debug.Log("Enemy Killed, reset DashCoolDown");
            Player.ResetDashCoolDown();
            //Détruire l'ennemi 
        }
    }
}