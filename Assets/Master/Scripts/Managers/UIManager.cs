using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Master.Scripts.Internal;
using PlayerComponent = Master.Scripts.Player.Player;

namespace Master.Scripts.Managers
{
    public class UIManager: SingletonMonoBehaviour<UIManager>
    {
        [Header("References")]
        [SerializeField] private Slider _healthGaugeLeft;
        [SerializeField] private Slider _healthGaugeRight;
        [SerializeField] private TMP_Text _scoreMeter;

        [Header("Animation durations")] 
        [SerializeField] private float _healthGaugeAnimTime;

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

        private void OnEnable()
        {
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

        private void UpdateScoreMeter(PlayerComponent ctx)
        {
            _scoreMeter.text = ctx.Score.ToString(@"000\.00 'm'"); // https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-numeric-format-strings
        }
    }
}
