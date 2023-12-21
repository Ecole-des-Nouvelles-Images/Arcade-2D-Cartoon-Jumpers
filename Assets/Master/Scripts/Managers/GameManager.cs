using System;
using UnityEngine;

using Master.Scripts.Internal;

namespace Master.Scripts.Managers
{
    public class GameManager: SingletonMonoBehaviour<GameManager>
    {
        [Header("Score System")]
        [SerializeField] private GameObject _player;
        [SerializeField] private float _scoreDenominator = 10f;

        public float Denominator => _scoreDenominator;

        private void OnValidate()
        {
            if (_scoreDenominator == 0) {
                _scoreDenominator = 1;
                throw new DivideByZeroException("Trying to set Score Denominator to 0. Fallback to 1.");
            }
        }
        
        public static Action<bool> OnPause;
        public static Action<float> OnScoreChanged;
        
        public static bool IsPaused { get; set; }
        public float Score { get; private set; }
        
        // ====================== //

        private void Start()
        {
            IsPaused = false;
        }

        private void Update()
        {
            UpdateScore();
        }

        // Methods //

        public static void SetPause()
        {
            IsPaused = IsPaused == false;
            Time.timeScale = IsPaused ? 0 : 1;
            OnPause.Invoke(IsPaused);
        }
        
        private void UpdateScore()
        {
            float currentHeight = _player.transform.position.y / _scoreDenominator;

            if (currentHeight < Score) return;
            
            Score = currentHeight;
            OnScoreChanged.Invoke(Score);
        }
    }
}
