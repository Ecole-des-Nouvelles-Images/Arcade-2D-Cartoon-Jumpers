using System;
using UnityEngine;

namespace Master.Scripts.Environment
{

    
    public class InfiniteParallax : MonoBehaviour
    {
        private enum Mode { Single = 1, Multiple = 2 }

        [SerializeField] private Mode _mode;
        [SerializeField] private float _factor = 1.0f;

        [Header("Settings")]
        [SerializeField] private float _heightCheckTolerance = 1f;
        
        // Mode : Single

        private SpriteRenderer _background;
        
        // Mode : Multiple
    
        private Transform _camera;
        private SpriteRenderer _sr1;
        private SpriteRenderer _sr2;
        private SpriteRenderer _sr3;

        private float SpriteHeight => _sr1.bounds.size.y;
        public SpriteRenderer Lowest {
            get {
                float minPosY = Mathf.Min(_sr1.transform.position.y, _sr2.transform.position.y, _sr3.transform.position.y);
                
                if (Math.Abs(minPosY - _sr1.transform.position.y) < _heightCheckTolerance)
                    return _sr1;
                if (Math.Abs(minPosY - _sr2.transform.position.y) < _heightCheckTolerance)
                    return _sr2;
            
                return _sr3;
            }
        }
        public SpriteRenderer Highest {
            get {
                float maxPosY = Mathf.Max(_sr1.transform.position.y, _sr2.transform.position.y, _sr3.transform.position.y);
                
                if (Math.Abs(maxPosY - _sr1.transform.position.y) < _heightCheckTolerance)
                    return _sr1;
                if (Math.Abs(maxPosY - _sr2.transform.position.y) < _heightCheckTolerance)
                    return _sr2;
            
                return _sr3;
            }
        }
        
        // Common properties

        public bool CameraIsInMiddle => _camera.position.y > Lowest.transform.position.y && _camera.position.y < Highest.transform.position.y;
    
        private float _previousCameraPosition;

        private void Start()
        {
            if (UnityEngine.Camera.main == null)
                throw new NullReferenceException("Camera.main access is null !");

            _camera = UnityEngine.Camera.main.transform;
            
            switch (_mode)
            {
                case Mode.Single:
                    SetupSingleFollow();
                    break;
                case Mode.Multiple:
                    SetupMultipleLayers();
                    break;
                default:
                    throw new Exception($"Unknow {_mode.ToString()} mode at object {name}");
            }
        }

        private void Update()
        {
            float cameraDelta = _camera.position.y - _previousCameraPosition;

            switch (_mode)
            {
                case Mode.Multiple:
                    MoveSprite(_sr1, cameraDelta);
                    MoveSprite(_sr2, cameraDelta);
                    MoveSprite(_sr3, cameraDelta);
                    break;
                case Mode.Single:
                    MoveSprite(_background, cameraDelta);
                    break;
                default:
                    throw new Exception($"Unknow {_mode.ToString()} mode at object {name}");
            }
        
            _previousCameraPosition = _camera.position.y;
        }
        
        // Setup //

        private void SetupMultipleLayers()
        {
            _sr1 = transform.GetChild(0).GetComponent<SpriteRenderer>();
            _sr2 = transform.GetChild(1).GetComponent<SpriteRenderer>();
            _sr3 = transform.GetChild(2).GetComponent<SpriteRenderer>();

            SetInitialPositions();
        }

        private void SetupSingleFollow()
        {
            _background = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        
        // Methods //
    
        private void SetInitialPositions()
        {
            _sr3.transform.Translate(0, SpriteHeight, 0);
            _sr1.transform.Translate(0, -SpriteHeight, 0);
        }

        private void MoveSprite(SpriteRenderer sr, float delta)
        {
            Vector3 srPosition = sr.transform.position;
        
            sr.transform.position = new Vector3(srPosition.x, srPosition.y + (delta * _factor), srPosition.z);
        }

        public void Reposition(SpriteRenderer sr)
        {
            SpriteRenderer srToMove;
            
            if (sr == Highest)
            {
                srToMove = Lowest;
                Debug.Log($"Should reposition {srToMove.name}");
                Vector3 position = srToMove.transform.position + new Vector3(0, SpriteHeight * 2, 0);
                Instantiate(srToMove, position, Quaternion.identity, transform);
                //srToMove.transform.Translate(0, SpriteHeight * 2, 0);
                //srToMove.transform.position = Highest.transform.position + new Vector3(0, SpriteHeight, 0);
            }
            else if (sr == Lowest)
            {
                srToMove = Highest;
                Debug.Log($"Should reposition {srToMove.name}");
                Vector3 position = srToMove.transform.position + new Vector3(0, -SpriteHeight * 2, 0);
                Instantiate(srToMove, position, Quaternion.identity, transform);
                //srToMove.transform.Translate(0, -SpriteHeight * 2, 0);
                // srToMove.transform.position = Lowest.transform.position - new Vector3(0, SpriteHeight, 0);
            }
        }
    }
}
