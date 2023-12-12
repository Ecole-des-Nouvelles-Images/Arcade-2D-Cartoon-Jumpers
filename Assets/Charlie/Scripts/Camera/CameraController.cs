using System.Collections;
using UnityEngine;
using Cinemachine;

using PlayerComponent = Charlie.Scripts.Player.Player;

namespace Charlie.Scripts.Camera
{
    [RequireComponent(typeof (CinemachineVirtualCamera))]
    public class CameraController: MonoBehaviour
    {
        [Header("Vertical offset when climb or fall")]
        [SerializeField] private float _offsetUp;
        [SerializeField] private float _offsetFall;
        
        [Space(5)] [Header("Un-linear duration of the linear interpolation")]
        [SerializeField] private float _dragDurationUp = 1f;
        [SerializeField] private float _dragDurationDown = 1f;
        
        [Header("Velocity deadzone where the camera is centered on the Player")]
        [Tooltip("Note: Include the negative value")]
        [SerializeField] private float _deadzoneThreshold;
        
        // ======================= //
        
        private CinemachineVirtualCamera _vCam;
        private CinemachineFramingTransposer _body;

        public static float DeadzoneThreshold { get; private set; }

        private void Awake()
        {
            _vCam = GetComponent<CinemachineVirtualCamera>();
            
            CinemachineComponentBase stageMode = _vCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
            if (stageMode is CinemachineFramingTransposer component)
                _body = component;

            DeadzoneThreshold = _deadzoneThreshold;
        }

        private void OnEnable()
        {
            PlayerComponent.OnDirectionChange += OnDirectionChange;
        }
        
        private void OnDisable()
        {
            PlayerComponent.OnDirectionChange -= OnDirectionChange;
        }
        
        private void OnDirectionChange(int direction)
        {
            switch (direction)
            {
                case < 0 :
                    SmoothOffsetToward(-_offsetFall);
                    break;
                case >= 0 :
                    SmoothOffsetToward(_offsetUp);
                    break;
            }
        }

        private void SmoothOffsetToward(float targetOffset)
        {
            StopAllCoroutines();
            StartCoroutine(LerpTrackingOffset(targetOffset));
        }
        
        private IEnumerator LerpTrackingOffset(float targetOffset)
        {
            float t = 0f;
            float duration = (targetOffset >= 0) ? _dragDurationUp : _dragDurationDown;
            // float initialOffset = _body.m_TrackedObjectOffset.y;

            while (t < 1)
            {
                _body.m_TrackedObjectOffset.y = Mathf.Lerp(_body.m_TrackedObjectOffset.y , targetOffset, t);
                t += Time.deltaTime / duration;
                yield return null;
            }
        }
    }   
}
