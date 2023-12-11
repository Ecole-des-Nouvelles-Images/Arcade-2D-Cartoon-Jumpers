using System.Collections.Generic;
using System.Linq;
using Master.Scripts.Environment;
using UnityEngine;

public class SectionManager : MonoBehaviour
{
    public GameObject[] sectionPrefabs;
    public GameObject UpgradeSectionPrefab; 
    public GameObject player;
    public float sectionSize = 21f;
    public int maxSectionsAhead = 3;
    public int maxSectionsBehind = 3;
    public float triggerPosition = 30; // Hauteur à laquelle le joueur doit être pour activer OnReachedSection

    private int triggeredSectionCount = 0;

    private Queue<GameObject> _activeSections = new Queue<GameObject>();
    private bool _sectionManagementStarted = false; // Booléen permet d'activer ou désactiver OnReachedSection

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
            if (i < maxSectionsAhead + maxSectionsBehind)
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
        //  Verification que le joueur a passé les premieres sections et n'a pas déjà trigger cette section avant, 
        if (_sectionManagementStarted && !sectionInstance.GetComponent<SectionColliderTrigger>().hasBeenTriggered )
        //if(!sectionInstance.GetComponent<SectionColliderTrigger>().hasBeenTriggered && sectionInstance != _activeSections.Peek()  )
        {
            
            Destroy(_activeSections.Dequeue());
            triggeredSectionCount++; 

            if (_activeSections.Count < maxSectionsAhead + maxSectionsBehind)
            {
                //  si triggeredSectionCount est à plus de 10, génération section d'upgrade
                if (triggeredSectionCount > 10)
                {
                    GameObject nextSectionPrefab = UpgradeSectionPrefab;
                    Vector3 newPosition = _activeSections.Last().transform.position + new Vector3(0, sectionSize, 0);
                    GameObject newSectionInstance = Instantiate(nextSectionPrefab, newPosition, Quaternion.identity);
                    _activeSections.Enqueue(newSectionInstance);
                    SectionColliderTrigger colliderTrigger = newSectionInstance.AddComponent<SectionColliderTrigger>();
                    colliderTrigger.Initialize(this);
                    triggeredSectionCount = 0;
                }
                else
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
        else if (_sectionManagementStarted && sectionInstance == _activeSections.Peek()) Debug.Log($"Player dead");
        
        
       
    }
}