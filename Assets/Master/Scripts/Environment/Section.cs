using System;
using UnityEngine;

namespace Master.Scripts.Environment
{
    [RequireComponent(typeof (Collider2D))]
    public class Section : MonoBehaviour
    {
        [Header("Difficulty")]
        public Difficulty Type;
        
        private SectionManager _manager;
        private Collider2D _collider;

        private void Awake()
        {
            _manager = transform.parent.GetComponent<SectionManager>();
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player") && _collider.enabled) return;
            
            _manager.OnSectionEnter(this);
            _collider.enabled = false;
        }
    }
}
