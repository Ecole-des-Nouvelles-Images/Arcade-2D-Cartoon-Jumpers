using UnityEngine;
using Cinemachine;

namespace Charlie.Scripts
{
    public class CameraController: MonoBehaviour
    {
        [Header("Offset when target standing on a surface")]
        public float OffsetY = 2f;

        private CinemachineVirtualCamera _vCam;

        private void OnEnable()
        {
            SurfaceHitbox.OnStandOnSurface += DragCameraUpward;
            SurfaceHitbox.OnStandInAir += ResetCameraOffset;
        }
        
        private void OnDisable()
        {
            SurfaceHitbox.OnStandOnSurface -= DragCameraUpward;
            SurfaceHitbox.OnStandInAir -= ResetCameraOffset;
        }

        private void AdjustCameraOffset(float offset)
        {
            CinemachineComponentBase transposer = _vCam.GetCinemachineComponent(CinemachineCore.Stage.Body);

            if (transposer is CinemachineComposer composer)
                composer.m_TrackedObjectOffset.y = offset;
        }

        private void DragCameraUpward()
        {
            AdjustCameraOffset(OffsetY);
        }

        private void ResetCameraOffset()
        {
            AdjustCameraOffset(0f);
        }
    }
}
