using System;
using System.Collections;
using UnityEngine;
using Cinemachine;

using Master.Scripts.Player;

namespace Master.Scripts.Camera
{
    [RequireComponent(typeof (CinemachineVirtualCamera))]
    public class CameraController: MonoBehaviour
    {
        [Header("Vertical offset when target standing on a surface")]
        public float OffsetY = 2f;
        
        [Space(5)] [Header("Duration of the linear interpolation")]
        [Tooltip("Note: Time is NOT in seconds")]
        public float DragDuration = 1f;
        
        [Space(5)] [Header("Delay before the upward drag")]
        public float StandingDelay = 2f;

        private CinemachineVirtualCamera _vCam;

        private void Awake()
        {
            _vCam = GetComponent<CinemachineVirtualCamera>();
        }

        private void OnEnable()
        {
            SurfaceHitbox.OnStandOnSurface += DragCameraUpward;
        }
        
        private void OnDisable()
        {
            SurfaceHitbox.OnStandOnSurface -= DragCameraUpward;
        }

        private IEnumerator AdjustCameraOffset(float targetOffset)
        {
            CinemachineComponentBase stageMode = _vCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
            float t = 0f;

            if (stageMode is not CinemachineFramingTransposer body)
                throw new Exception("Error: Camera.body stage mode is not FramingTransposer or could not be getted");

            if (targetOffset > 0f)
                yield return new WaitForSeconds(StandingDelay);
            
            while (t < 1)
            {
                t += Time.deltaTime / DragDuration;
                body.m_TrackedObjectOffset.y = Mathf.Lerp(body.m_TrackedObjectOffset.y, targetOffset, t);
                yield return null;
            }
        }
        
        private void DragCameraUpward()
        {
            StopAllCoroutines();
            StartCoroutine(AdjustCameraOffset(OffsetY));
        }

        private void ResetCameraOffset()
        {
            StopAllCoroutines();
            StartCoroutine(AdjustCameraOffset(0f));
        }
    }
}
