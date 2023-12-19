using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Master.Scripts.Environment
{
    public class SectionManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _basePrefab;
        [SerializeField] private GameObject _checkpointPrefab;
        [SerializeField] private List<GameObject> _bonusZonePrefabs = new ();

        [Header("Gameplay")] 
        [SerializeField] [Range(1, 20)] private int _checkpointFrequency;
        [SerializeField] [Range(1, 20)] private int _bonusZoneFrequencyMin;
        [SerializeField] [Range(1, 20)] private int _bonusZoneFrequencyMax;

        [Header("Parameters")]
        [SerializeField] private int _maxSectionsAhead = 1;
        [SerializeField] private int _maxSectionsBehind = 1;
        [SerializeField] private float _triggerPosition = 30; // Hauteur à laquelle le joueur doit être pour activer OnReachedSection

        // Properties
        
        private float SectionSize => _basePrefab.GetComponent<Section>().Size;

        private readonly Queue<GameObject> _activeSections = new ();
        private int _sectionIndex = 1;
        private bool _sectionManagementStarted = false; // Booléen permet d'activer ou désactiver OnReachedSection

        private void Start()
        {
            for (int i = 0; i < _maxSectionsAhead + _maxSectionsBehind; i++) {
                GenerateSections();
            }
        }

        private void Update()
        {
            if (!_sectionManagementStarted && _player.transform.position.y >= _triggerPosition)
            {
                Debug.Log($"Player went higher than triggerPosition");
                _sectionManagementStarted = true;
            }
        }

        private void GenerateSections()
        {
            Vector3 sectionPosition = new (0, SectionSize * _sectionIndex, 0);
            GameObject section;

            if (_sectionIndex % _checkpointFrequency == 0) {
                section = Instantiate(_checkpointPrefab, sectionPosition, Quaternion.identity, transform);
            }
            else if (_sectionIndex % Random.Range(_bonusZoneFrequencyMin, _bonusZoneFrequencyMax) == 0 && _sectionIndex % _checkpointFrequency != 0 ) {
                section = Instantiate(_bonusZonePrefabs[Random.Range(0, _bonusZonePrefabs.Count)], sectionPosition, Quaternion.identity, transform);
            }
            else
            {
                section = Instantiate(_basePrefab, sectionPosition, Quaternion.identity, transform);
            }

            section.GetComponent<Section>().OnSectionEnter += OnSectionEnter;

            _sectionIndex++;
            if (_activeSections.Count == _maxSectionsAhead + _maxSectionsBehind + 1)
            {
                _activeSections.Enqueue(section);
                Destroy(_activeSections.Dequeue());
            }
            
            /* for (int i = 0; i < count; i++)
            {
                if (i < _maxSectionsAhead + _maxSectionsBehind)
                {
                    GameObject nextSectionPrefab = _sectionPrefabs[Random.Range(0, _sectionPrefabs.Length)];
                    GameObject nextSection = Instantiate(nextSectionPrefab, new Vector3(0, i * _sectionSize, 0), Quaternion.identity, this.transform);
                    _activeSections.Enqueue(nextSection);
                    SectionColliderTrigger colliderTrigger = nextSection.AddComponent<SectionColliderTrigger>();
                    colliderTrigger.Initialize(this);
                }
            } */
        }

        private void OnSectionEnter(Section obj)
        {
            foreach (GameObject section in _activeSections)
            {
                
            }
        }

        /* public void OnSectionReached(GameObject sectionInstance)
        {   
            
            /*  Verification que le joueur a passé les premieres sections et n'a pas déjà trigger cette section avant, 
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
        } */
    }
}
