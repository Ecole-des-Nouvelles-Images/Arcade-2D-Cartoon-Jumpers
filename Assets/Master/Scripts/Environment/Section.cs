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

        private void Awake()
        {
            _manager = transform.parent.GetComponent<SectionManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            _manager.OnSectionEnter(this);
        }
    }
}
