using System;
using UnityEngine;

namespace Master.Scripts.Environment
{
    [RequireComponent(typeof (Collider2D))]
    public class Section : MonoBehaviour
    {
        public Action<Section> OnSectionEnter;

        private Collider2D _collider;
        public float Size { get; private set; }

        public void Awake()
        {
            _collider = GetComponent<Collider2D>();
            Size = _collider.bounds.size.y;
        }

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
