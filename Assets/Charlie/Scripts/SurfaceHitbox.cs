using System;
using UnityEngine;

namespace Charlie.Scripts
{
    public class SurfaceHitbox: MonoBehaviour
    {
        public static Action OnStandOnSurface;
        public static Action OnStandInAir;
        
        public bool IsOnSurface { get; private set; }
        
        private int _objectCount;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Ground")) return;

            _objectCount++;
            IsOnSurface = true;
            OnStandOnSurface.Invoke();
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Ground")) return;

            _objectCount--;

            if (_objectCount == 0)
            {
                IsOnSurface = false;
                OnStandInAir.Invoke();
            }
        }
    }
}
