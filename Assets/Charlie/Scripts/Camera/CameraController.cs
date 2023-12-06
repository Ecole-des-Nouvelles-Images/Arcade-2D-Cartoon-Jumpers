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
        public float OffsetUp;
        public float OffsetFall;
        
        [Space(5)] [Header("Duration of the linear interpolation")]
        [Tooltip("Note: Time is NOT in seconds")]
        public float DragDuration = 1f;
        
        [Space(5)]
        [Tooltip("Delay before the upward drag")] public float StandingDelay = 2f;
        [Tooltip("Threshold below which camera will not drag around")] public float VelocityThreshod;
        
        // ======================= //

        private CinemachineVirtualCamera _vCam;
        private CinemachineFramingTransposer _body;

        private void Awake()
        {
            _vCam = GetComponent<CinemachineVirtualCamera>();
            
            CinemachineComponentBase stageMode = _vCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
            if (stageMode is CinemachineFramingTransposer component)
                _body = component;
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
            Debug.Log($"Velocity changed to [{direction}]");
            switch (direction)
            {
                case < 0 :
                    SmoothOffsetToward(OffsetFall);
                    break;
                case 0 :
                    SmoothOffsetToward(0f);
                    break;
                case > 0 :
                    SmoothOffsetToward(OffsetUp);
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
            float initialOffset = _body.m_TrackedObjectOffset.y;

            while (t < 1)
            {
                _body.m_TrackedObjectOffset.y = Mathf.Lerp(initialOffset, targetOffset, t);
                t += Time.deltaTime / DragDuration;
                yield return null;
            }
        }
    }   
}
