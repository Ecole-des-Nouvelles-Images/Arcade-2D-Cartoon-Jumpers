using System;
using UnityEngine;

using Master.Scripts.Internal;
using Master.Scripts.PlayerManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Master.Scripts.Managers
{
    public class GameManager: SingletonMonoBehaviour<GameManager>
    {
        [Header("Score System")]
        [SerializeField] private GameObject _player;
        [SerializeField] private float _scoreDenominator = 10f;

        public EventSystem eventSystem;
        public InputAction playerInput;
        
        public float Denominator => _scoreDenominator;

        private void OnValidate()
        {
            if (_scoreDenominator == 0) {
                _scoreDenominator = 1;
                throw new DivideByZeroException("Trying to set Score Denominator to 0. Fallback to 1.");
            }
        }
        
        public Action<bool> OnPause;
        public Action<float> OnScoreChanged;
        public bool IsPaused { get; set; }
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

        public void SetPause()
        {
            PlayerController ctrl = _player.GetComponent<PlayerManagement.Player>().Controller;

            this.IsPaused = !IsPaused;

            if (IsPaused)
            {
                ctrl.Controls.UIMenu.Enable();
                ctrl.Controls.GamePlay.Disable();
            }
            else
            {
                ctrl.Controls.UIMenu.Disable();
                ctrl.Controls.GamePlay.Enable();
            }

            Time.timeScale = IsPaused ? 0 : 1;
            OnPause.Invoke(IsPaused);
            // SwitchCurrentActionMap(IsPaused ? "UI" : "Player");
            //eventSystem.playerInput.SwitchCurrentActionMap(IsPaused ? "UI" : "Player");
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
