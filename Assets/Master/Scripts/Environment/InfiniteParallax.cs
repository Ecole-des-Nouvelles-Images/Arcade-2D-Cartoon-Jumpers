using System;
using UnityEngine;

namespace Master.Scripts.Environment
{

    public class InfiniteParallax : MonoBehaviour
    {
        [SerializeField] private float _factor = 1.0f;
    
        private Transform _camera;
        private SpriteRenderer _sr1;
        private SpriteRenderer _sr2;
        private SpriteRenderer _sr3;

        private float SpriteHeight => _sr1.bounds.size.y;
        public SpriteRenderer Lowest {
            get {
                float minPosY = Mathf.Min(_sr1.transform.position.y, _sr2.transform.position.y, _sr3.transform.position.y);
            
                if (Math.Abs(minPosY - _sr1.transform.position.y) < 0)
                    return _sr1;
                if (Math.Abs(minPosY - _sr2.transform.position.y) < 0)
                    return _sr2;
            
                return _sr3;
            }
        }
        public SpriteRenderer Highest {
            get {
                float maxPosY = Mathf.Max(_sr1.transform.position.y, _sr2.transform.position.y, _sr3.transform.position.y);
            
                if (Math.Abs(maxPosY - _sr1.transform.position.y) < 0)
                    return _sr1;
                if (Math.Abs(maxPosY - _sr2.transform.position.y) < 0)
                    return _sr2;
            
                return _sr3;
            }
        }

        public bool CameraIsInMiddle => _camera.position.y > Lowest.transform.position.y && _camera.position.y < Highest.transform.position.y;
    
        private float _previousCameraPosition;

        private void Start()
        {
            if (UnityEngine.Camera.main == null)
                throw new NullReferenceException("Camera.main access is null !");

            _camera = UnityEngine.Camera.main.transform;
            _sr1 = transform.GetChild(0).GetComponent<SpriteRenderer>();
            _sr2 = transform.GetChild(1).GetComponent<SpriteRenderer>();
            _sr3 = transform.GetChild(2).GetComponent<SpriteRenderer>();

            SetInitialPositions();
        }

        private void Update()
        {
            float cameraDelta = _camera.position.y - _previousCameraPosition;

            MoveSprite(_sr1, cameraDelta);
            MoveSprite(_sr2, cameraDelta);
            MoveSprite(_sr3, cameraDelta);
        
            _previousCameraPosition = _camera.position.y;
        }
    
        private void SetInitialPositions()
        {
            Vector3 initialPositionSr1 = _sr1.transform.position;

            _sr2.transform.position = new Vector3(initialPositionSr1.x, initialPositionSr1.y + SpriteHeight, initialPositionSr1.z);
            _sr3.transform.position = new Vector3(initialPositionSr1.x, initialPositionSr1.y - SpriteHeight, initialPositionSr1.z);
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
                srToMove.transform.position = Highest.transform.position + new Vector3(0, SpriteHeight, 0);
            }
            else if (sr == Lowest)
            {
                srToMove = Highest;
                srToMove.transform.position = Lowest.transform.position - new Vector3(0, SpriteHeight, 0);
            }
        }
    }
}
