using Alex.Scripts.Enemies;
using UnityEngine;

namespace Alex.Scripts.Enemies
{
    [CreateAssetMenu(fileName = "New Move Command", menuName = "Move Command")]
    public class MoveCommand : ScriptableObject, IMoveCommand
    {
        public Vector2 _destination; // Renseigne la destination 

        public Vector2 Execute(Transform transform)
        {
            return _destination; // Renvoyer la destionation au controller de l'enemy, l'appliquer ici ne permet pas d'utiliser une boucle update pour le déplacement 
        }
    }
}