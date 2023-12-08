using System;
using UnityEngine;

namespace Master.Scripts.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class SurfaceHitbox: MonoBehaviour
    {
        public static Action OnStandOnSurface;
        
        public bool IsOnSurface { get; private set; }
        
        private int _objectCount;

        private void Awake()
        {
            if (!GetComponent<Collider2D>().isTrigger)
            {
                Debug.LogWarning($"{transform.parent.gameObject.name}.{gameObject.name} collider is not trigger !");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Wall")) return;

            _objectCount++;
            IsOnSurface = true;
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Wall")) return;

            _objectCount--;

            if (_objectCount == 0)
            {
                IsOnSurface = false;
            }
        }
    }
}
