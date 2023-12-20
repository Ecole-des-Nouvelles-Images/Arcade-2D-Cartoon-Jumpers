using System;
using UnityEngine;

namespace Master.Scripts.Environment
{
    [RequireComponent(typeof (Collider2D))]
    public class Section : MonoBehaviour
    {
        public Action<Section> OnSectionEnter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            // OnSectionEnter.Invoke(this);
        }

        private void OnDestroy()
        {
            OnSectionEnter = null;
        }
    }
}
