using UnityEngine;

namespace Master.Scripts.Environment
{
    public class SectionColliderTrigger : MonoBehaviour
    {
        public bool HasBeenTriggered { get; private set; }
        
        private SectionManager _sectionManager;

        public void Initialize(SectionManager manager)
        {
            _sectionManager = manager;
            GetComponent<Collider2D>().isTrigger = true;
            HasBeenTriggered = false; 
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            Debug.Log($"Player detected {this.name}");
            _sectionManager.OnSectionReached(gameObject);
            HasBeenTriggered = true; 
        }
    }
}
