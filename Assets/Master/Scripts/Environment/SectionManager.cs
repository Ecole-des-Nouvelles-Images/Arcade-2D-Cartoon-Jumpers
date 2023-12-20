using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Master.Scripts.Environment
{
    public class SectionManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _startSection;
        [SerializeField] private GameObject _checkpointPrefab;
        [SerializeField] private GameObject _rootPrefab;
        [SerializeField] private List<GameObject> _basePrefabs;
        [SerializeField] private List<GameObject> _bonusZonePrefabs = new ();

        [Header("Gameplay")] 
        [SerializeField] [Range(1, 20)] private int _checkpointFrequency;
        [SerializeField] [Range(1, 20)] private int _bonusZoneFrequencyMin;
        [SerializeField] [Range(1, 20)] private int _bonusZoneFrequencyMax;

        [Header("Parameters")]
        [SerializeField] private int _initialSectionsAhead = 2;
        [SerializeField] [Min(1)] private int _maxSectionAlive = 3;
        
        private void OnValidate()
        {
            if (_initialSectionsAhead + 1 > _maxSectionAlive)
            {
                _initialSectionsAhead = _maxSectionAlive - 1;
                Debug.LogWarning("SectionManager: Unable to initialize more sections than the maximum sections alive");
            }
        }
        
        // Properties

        private readonly Queue<GameObject> _activeSections = new ();
        
        private const float SectionSize = 28.25f;
        
        private int _sectionIndex = 1;
        private int _bonusSectionDenominator;

        private void Start()
        {
            _bonusSectionDenominator = Random.Range(_bonusZoneFrequencyMin, _bonusZoneFrequencyMax);
            _activeSections.Enqueue(_startSection);
            
            for (int i = 0; i < _initialSectionsAhead; i++) {
                _activeSections.Enqueue(GenerateSection());
            }
        }

        private void Update()
        {
        }

        private GameObject GenerateSection()
        {
            Vector3 sectionPosition = new (0, SectionSize * _sectionIndex, 0);
            GameObject section;
            
            if (_sectionIndex % _checkpointFrequency == 0)
            {
                section = Instantiate(_checkpointPrefab, transform, true);
            }
            else if (_sectionIndex % _bonusSectionDenominator == 0 && _sectionIndex % _checkpointFrequency != 0 )
            {
                GameObject bonusSectionPrefab = _bonusZonePrefabs[Random.Range(0, _bonusZonePrefabs.Count)];
                section = Instantiate(bonusSectionPrefab,transform, true);
                _bonusSectionDenominator = Random.Range(_bonusZoneFrequencyMin, _bonusZoneFrequencyMax);
            }
            else
            {
                List<GameObject> availablePrefabs;

                switch (_sectionIndex)
                {
                    case > 1 and <= 3:
                        availablePrefabs = GetSectionPrefabsByDifficulty(Difficulty.Chill);
                        break;
                    case <= 10:
                        availablePrefabs = GetSectionPrefabsByDifficulty(Difficulty.Easy);
                        break;
                    default:
                        availablePrefabs = new List<GameObject> { _rootPrefab };
                        Debug.LogWarning("Instanciated empty Root prefab section");
                        break;
                }
                
                if (availablePrefabs.Count == 0) Debug.Log("availablePrefabs is empty");
                else
                {
                    Debug.Log($"Current item count in list in : {availablePrefabs.Count}");
                }
                GameObject prefabToUse = availablePrefabs[Random.Range(0, availablePrefabs.Count)];
                section = Instantiate(prefabToUse, transform, true);
            }
            
            section.transform.position = sectionPosition;
            
            _sectionIndex++;
            return section;
        }

        private List<GameObject> GetSectionPrefabsByDifficulty(Difficulty type)
        {
            return _basePrefabs.Where(prefab => prefab.GetComponent<Section>().Type == type).ToList();
        }

        public void OnSectionEnter(Section obj)
        {
            if (_activeSections.Count == _maxSectionAlive)
            {
                Destroy(_activeSections.Dequeue());
            }
            
            _activeSections.Enqueue(GenerateSection());
        }
    }
}
