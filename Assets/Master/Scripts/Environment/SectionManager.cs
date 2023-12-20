using System;
using System.Collections.Generic;
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

        private const float SectionSize = 28.25f;
        private int _bonusSectionDenominator;

        private readonly Queue<GameObject> _activeSections = new ();
        private int _sectionIndex = 1;
        private bool _sectionManagementStarted = false; // Booléen permet d'activer ou désactiver OnReachedSection

        private void Start()
        {
            _bonusSectionDenominator = Random.Range(_bonusZoneFrequencyMin, _bonusZoneFrequencyMax);
            
            for (int i = 0; i < _maxSectionsAhead + _maxSectionsBehind; i++) {
                Debug.Log("Should generate sections...");
                GenerateSections();
            }
        }

        private void Update()
        {
        }

        private void GenerateSections()
        {
            Vector3 sectionPosition = new (0, SectionSize * _sectionIndex, 0);
            GameObject section;
            
            if (_sectionIndex % _checkpointFrequency == 0)
            {
                section = Instantiate(_checkpointPrefab, transform, true);
                section.transform.position = sectionPosition;
                Debug.Log($"Section should be a {_checkpointPrefab.name} at : {sectionPosition}");
            }
            else if (_sectionIndex % _bonusSectionDenominator == 0 && _sectionIndex % _checkpointFrequency != 0 )
            {
                GameObject bonusSectionPrefab = _bonusZonePrefabs[Random.Range(0, _bonusZonePrefabs.Count)];
                section = Instantiate(bonusSectionPrefab,transform, true);
                section.transform.position = sectionPosition;
                _bonusSectionDenominator = Random.Range(_bonusZoneFrequencyMin, _bonusZoneFrequencyMax);
                Debug.Log($"Section should be a {bonusSectionPrefab.name} at : {sectionPosition}");
            }
            else
            {
                section = Instantiate(_basePrefab,transform, true);
                section.transform.position = sectionPosition;
                Debug.Log($"Section should be a {_basePrefab.name} at : {sectionPosition}");
            }

            try {
                section.GetComponent<Section>().OnSectionEnter += OnSectionEnter;
            }
            catch {
                throw new Exception("\"Section\" component missing on a section prefab");
            }
            
            _sectionIndex++;
        }

        private void OnSectionEnter(Section obj)
        {
            
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
