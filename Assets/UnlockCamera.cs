using System;
using System.Collections;
using System.Collections.Generic;
using Master.Scripts.Camera;
using UnityEngine;

public class UnlockCamera : MonoBehaviour
{
    [SerializeField] private CameraController _camera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _camera.DisablePlayerTracking();
    }
}
