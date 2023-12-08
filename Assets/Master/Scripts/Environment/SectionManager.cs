using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Scripts.Environment
{
    public class SectionManager : MonoBehaviour
    {
        public GameObject[] SectionPrefab;
        public float sectionSize = 21f;
        public int MaxSectionsAhead = 3;
        public int MaxSectionsBehind = 3;
        private Queue<GameObject> sectionQueue = new Queue<GameObject>();
        private Queue<GameObject> activeSections = new Queue<GameObject>();

        private void Start()
        {
            foreach (GameObject sectionPrefabs in SectionPrefab)
            {
                sectionQueue.Enqueue(sectionPrefabs);
            }

            GenerateSections(MaxSectionsAhead + MaxSectionsBehind);
        }

        void GenerateSections(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject nextSection = sectionQueue.Dequeue();
                GameObject sectionInstance =
                    Instantiate(nextSection, new Vector3(0, i * sectionSize, 0), Quaternion.identity);
                sectionQueue.Enqueue(nextSection);
                activeSections.Enqueue(sectionInstance);
                //Collider2D[] colliders = sectionInstance.GetComponentsInChildren<Collider2D>();
                //foreach ()
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            GameObject sectionToRemove = activeSections.Dequeue();
            Destroy(sectionToRemove);
            GameObject nextSection = sectionQueue.Dequeue();
            GameObject sectionInstance = Instantiate(nextSection,
                new Vector3(0, (activeSections.Count + MaxSectionsBehind) * sectionSize, 0), Quaternion.identity);
            sectionQueue.Enqueue(nextSection);
            activeSections.Enqueue(sectionInstance);
        }
    }
}