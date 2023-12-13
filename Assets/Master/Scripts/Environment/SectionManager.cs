using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Master.Scripts.Environment
{
    public class SectionManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject[] _sectionPrefabs;
        [SerializeField] private GameObject _upgradeSectionPrefab; 
        [SerializeField] private GameObject _player;
        
        [Header("Parameters")]
        [SerializeField] private float _sectionSize = 21f;
        [SerializeField] private int _maxSectionsAhead = 3;
        [SerializeField] private int _maxSectionsBehind = 3;
        [SerializeField] private float _triggerPosition = 30; // Hauteur à laquelle le joueur doit être pour activer OnReachedSection

        private int _triggeredSectionCount = 0;

        private readonly Queue<GameObject> _activeSections = new Queue<GameObject>();
        private bool _sectionManagementStarted = false; // Booléen permet d'activer ou désactiver OnReachedSection

        private void Start()
        {
            GenerateSections(_maxSectionsAhead + _maxSectionsBehind);
        }

        void Update()
        {
            if (!_sectionManagementStarted && _player.transform.position.y >= _triggerPosition)
            {
                Debug.Log($"Player went higher than triggerPosition");
                _sectionManagementStarted = true;
            }
        }

        void GenerateSections(int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (i < _maxSectionsAhead + _maxSectionsBehind)
                {
                    GameObject nextSectionPrefab = _sectionPrefabs[Random.Range(0, _sectionPrefabs.Length)];
                    GameObject nextSection = Instantiate(nextSectionPrefab, new Vector3(0, i * _sectionSize, 0), Quaternion.identity, this.transform);
                    _activeSections.Enqueue(nextSection);
                    SectionColliderTrigger colliderTrigger = nextSection.AddComponent<SectionColliderTrigger>();
                    colliderTrigger.Initialize(this);
                }
            }
        }

        public void OnSectionReached(GameObject sectionInstance)
        {   
            //  Verification que le joueur a passé les premieres sections et n'a pas déjà trigger cette section avant, 
            if (_sectionManagementStarted && !sectionInstance.GetComponent<SectionColliderTrigger>().HasBeenTriggered )
                //if(!sectionInstance.GetComponent<SectionColliderTrigger>().hasBeenTriggered && sectionInstance != _activeSections.Peek()  )
            {
                Destroy(_activeSections.Dequeue());
                _triggeredSectionCount++; 

                if (_activeSections.Count < _maxSectionsAhead + _maxSectionsBehind)
                {
                    //  si triggeredSectionCount est à plus de 10, génération section d'upgrade
                    if (_triggeredSectionCount > 10)
                    {
                        GameObject nextSectionPrefab = _upgradeSectionPrefab;
                        Vector3 newPosition = _activeSections.Last().transform.position + new Vector3(0, _sectionSize, 0);
                        GameObject newSectionInstance = Instantiate(nextSectionPrefab, newPosition, Quaternion.identity);
                        _activeSections.Enqueue(newSectionInstance);
                        SectionColliderTrigger colliderTrigger = newSectionInstance.AddComponent<SectionColliderTrigger>();
                        colliderTrigger.Initialize(this);
                        _triggeredSectionCount = 0;
                    }
                    else
                    {
                        GameObject nextSectionPrefab = _sectionPrefabs[Random.Range(0, _sectionPrefabs.Length)];
                        Vector3 newPosition = _activeSections.Last().transform.position + new Vector3(0, _sectionSize, 0);
                        GameObject newSectionInstance = Instantiate(nextSectionPrefab, newPosition, Quaternion.identity);
                        _activeSections.Enqueue(newSectionInstance);
                        SectionColliderTrigger colliderTrigger = newSectionInstance.AddComponent<SectionColliderTrigger>();
                        colliderTrigger.Initialize(this);
                    }

                }
            }
            else if (_sectionManagementStarted && sectionInstance == _activeSections.Peek())
            {
                Debug.Log($"Player dead");
            }
        }
    }
}
