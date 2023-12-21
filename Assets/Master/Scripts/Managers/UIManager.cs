using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Master.Scripts.Internal;
using Unity.VisualScripting;
using PlayerComponent = Master.Scripts.Player.Player;

namespace Master.Scripts.Managers
{
    public class UIManager: SingletonMonoBehaviour<UIManager>
    {
        [Header("References")] 
        [SerializeField] private Canvas _pauseMenu;
        [Space(5)]
        [SerializeField] private Slider _healthGaugeLeft;
        [SerializeField] private Slider _healthGaugeRight;
        [SerializeField] private TMP_Text _scoreMeter;
        

        [Header("Animation durations")] 
        [SerializeField] private float _healthGaugeAnimTime;
        [SerializeField] private float _pauseFadeTime;

        private float HealthGaugeMaster
        {
            get {
                if (Math.Abs(_healthGaugeLeft.value - _healthGaugeRight.value) > 0)
                    throw new Exception("Health gauges are not synchronised");
                
                return _healthGaugeLeft.value;
            }
            set {
                _healthGaugeLeft.value = value;
                _healthGaugeRight.value = value;
            }
        }
        private float PauseUIAlpha
        {
            get => _pauseUIRoot.alpha;
            set => _pauseUIRoot.alpha = value;
        }

        private CanvasGroup _pauseUIRoot;

        protected override void Awake()
        {
            base.Awake();
            _pauseUIRoot = _pauseMenu.transform.GetChild(0).GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            _pauseMenu.gameObject.SetActive(false);
            PauseUIAlpha = 0;
        }

        private void OnEnable()
        {
            GameManager.OnPause += ShowPauseMenu;
            PlayerComponent.OnHealthChanged += UpdateHealthGauge;
            PlayerComponent.OnScoreChanged += UpdateScoreMeter;
        }
        
        private void OnDisable()
        {
            PlayerComponent.OnHealthChanged -= UpdateHealthGauge;
            PlayerComponent.OnScoreChanged -= UpdateScoreMeter;
        }
        
        // Events Handlers //

        private void UpdateHealthGauge(PlayerComponent ctx)
        {
            float initialGaugeValue = HealthGaugeMaster;
            float targetGaugeValue = (float) ctx.Health / ctx.MaxHealth;

            StartCoroutine(UpdateHealthGaugeCoroutine(initialGaugeValue, targetGaugeValue));
        }

        private void UpdateScoreMeter(PlayerComponent ctx)
        {
            _scoreMeter.text = ctx.Score.ToString("000.00 'm'"); // https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-numeric-format-strings
        }
        
        private void ShowPauseMenu(bool enable)
        {
            StopCoroutine(FadePausePanelOutCoroutine());
            StopCoroutine(FadePausePanelInCoroutine());
            Debug.Log($"Called pause menu ! Should activate : {enable}");

            if (!enable) {
                StartCoroutine(FadePausePanelOutCoroutine());
                return;
            }

            _pauseMenu.gameObject.SetActive(true);
            StartCoroutine(FadePausePanelInCoroutine());
        }
        
        // Animations Coroutines //
        
        private IEnumerator UpdateHealthGaugeCoroutine(float initial, float target)
        {
            float t = 0f;

            while (t < 1)
            {
                HealthGaugeMaster = Mathf.Lerp(initial, target, t);
                t += Time.deltaTime / _healthGaugeAnimTime;
                yield return null;
            }
        }

        private IEnumerator FadePausePanelInCoroutine()
        {
            float t = 0f;
            float initialOpacity = PauseUIAlpha;
            float opacityTarget = 1;

            while (t < 1)
            {
                PauseUIAlpha = Mathf.Lerp(initialOpacity, opacityTarget, t);
                t += Time.unscaledDeltaTime / _pauseFadeTime;
                yield return null;
            }

            if (opacityTarget == 0)
                _pauseMenu.gameObject.SetActive(false);
        }
        
        private IEnumerator FadePausePanelOutCoroutine()
        {
            float t = 0f;
            float initialOpacity = PauseUIAlpha;
            float opacityTarget = 0;

            while (t < 1)
            {
                PauseUIAlpha = Mathf.Lerp(initialOpacity, opacityTarget, t);
                t += Time.unscaledDeltaTime / _pauseFadeTime;
                yield return null;
            }

            if (opacityTarget == 0)
                _pauseMenu.gameObject.SetActive(false);
        }
    }
}