using System.Collections.Generic;
using System.Linq;
using Master.Scripts.Environment;
using UnityEngine;

public class SectionManager : MonoBehaviour
{
    public GameObject[] sectionPrefabs;
    public GameObject player;
    public float sectionSize = 21f;
    public int maxSectionsAhead = 3;
    public int maxSectionsBehind = 3;
    public float triggerPosition = 30; // Hauteur à laquelle activer la gestion des sections

    private Queue<GameObject> _activeSections = new Queue<GameObject>();
    private bool _sectionManagementStarted = false; // Booléen pour déclencher la gestion des sections

    private void Start()
    {
        GenerateSections(maxSectionsAhead + maxSectionsBehind);
    }

    void Update()
    {
        if (!_sectionManagementStarted && player.transform.position.y >= triggerPosition)
        {
            Debug.Log($"Player went higher than triggerPosition");
            _sectionManagementStarted = true;
        }
    }

    void GenerateSections(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (i < maxSectionsAhead)
            {
                GameObject nextSectionPrefab = sectionPrefabs[Random.Range(0, sectionPrefabs.Length)];
                GameObject nextSection = Instantiate(nextSectionPrefab, new Vector3(0, i * sectionSize, 0), Quaternion.identity);
                _activeSections.Enqueue(nextSection);
                SectionColliderTrigger colliderTrigger = nextSection.AddComponent<SectionColliderTrigger>();
                colliderTrigger.Initialize(this);
            }
        }
    }

    public void OnSectionReached(GameObject sectionInstance)
    {
        if (_sectionManagementStarted)
        {
            Destroy(_activeSections.Dequeue());

            if (_activeSections.Count < maxSectionsAhead + maxSectionsBehind)
            {
                GameObject nextSectionPrefab = sectionPrefabs[Random.Range(0, sectionPrefabs.Length)];
                Vector3 newPosition = _activeSections.Last().transform.position + new Vector3(0, sectionSize, 0);
                GameObject newSectionInstance = Instantiate(nextSectionPrefab, newPosition, Quaternion.identity);
                _activeSections.Enqueue(newSectionInstance);
                SectionColliderTrigger colliderTrigger = newSectionInstance.AddComponent<SectionColliderTrigger>();
                colliderTrigger.Initialize(this);
            }
        }
    }
}