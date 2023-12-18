using System;
using UnityEngine;

namespace Master.Scripts.Environment
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ParallaxSection: MonoBehaviour
    {
        private InfiniteParallax _manager;
        private SpriteRenderer _sr;

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            _manager = transform.parent.GetComponent<InfiniteParallax>();

            if (_manager == null)
                throw new NullReferenceException("Unable to get InfiniteParallax parent component");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            _manager.Reposition(_sr);
        }
    }
}
