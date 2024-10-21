using UnityEngine;

namespace Master.Scripts.Environment
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            if (other.transform.position.y > transform.position.y)
            {
                _collider.isTrigger = false;
            }
        }
    }
}
