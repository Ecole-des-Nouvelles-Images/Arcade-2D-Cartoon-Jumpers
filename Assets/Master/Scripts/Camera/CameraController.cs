using System.Collections;
using UnityEngine;
using Cinemachine;

using PlayerComponent = Master.Scripts.Player.Player;

namespace Master.Scripts.Camera
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
        
        [Header("Velocity thresholds where the camera is centered on the Player")]
        [Tooltip("Note: Include the negative value")]
        [SerializeField] private float _fallThreshold;
        
        public static float VelocityThreshold { get; set; }
        
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
            if (direction < 0)
            {
                SmoothOffsetToward(-_offsetFall);
            }
            else
            {
                SmoothOffsetToward(_offsetUp);
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
            float duration = (targetOffset > 0) ? _dragDurationUp : _dragDurationDown;
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
