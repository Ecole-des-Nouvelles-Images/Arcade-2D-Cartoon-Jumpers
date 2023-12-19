using System;
using UnityEngine;

namespace Master.Scripts.Environment
{
    public class InfiniteParallax : MonoBehaviour
    {
        private enum Mode { Background = 1, Parallax = 2 }

        [SerializeField] private Mode _mode;
        [SerializeField] private float _factor = 1.0f;
        
        private SpriteRenderer _layer;
        private Transform _camera;
        private float _layerHeight;
        private float _previousCameraPosition;
        
        private void OnValidate()
        {
            if (_mode == Mode.Background && _factor != 1.0f)
            {
                _factor = 1.0f;
                Debug.LogWarning($"{typeof(InfiniteParallax)}: In \"Background\" mode, factor is locked to 1f.");
            }
        }

        private void Start()
        {
            if (UnityEngine.Camera.main == null)
                throw new NullReferenceException("Camera.main access is null !");

            _camera = UnityEngine.Camera.main.transform;

            if (_mode == Mode.Parallax) {
                _layer = transform.GetChild(0).GetComponent<SpriteRenderer>();
                _layerHeight = _layer.bounds.size.y;
            }
        }

        private void Update()
        {
            float cameraDelta = _camera.position.y - _previousCameraPosition;

            switch (_mode)
            {
                case Mode.Background:
                    transform.position = new Vector3(0, _camera.position.y, 0);
                    break;
                case Mode.Parallax:
                    Vector2 offset = new (0, cameraDelta / _layerHeight * _factor);
                    _layer.material.mainTextureOffset += offset; // Offset UV Texture directly;
                    break;
                default:
                    throw new Exception($"Unknow {_mode.ToString()} mode at object {name}");
            }
        
            _previousCameraPosition = _camera.position.y;
        }
    }
}
