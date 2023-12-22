using Master.Scripts.Camera;
using Master.Scripts.Managers;
using UnityEngine;

using PlayerComponent = Master.Scripts.Player.Player;

public class DeathZoneLimit : MonoBehaviour
{
    [SerializeField] private PlayerComponent _player;
    [SerializeField] private CameraController _camera;
    [SerializeField] private float _globalOffset;

    private float _maximumPosition;

    private void Start()
    {
        transform.Translate(0, _globalOffset, 0);
    }

    private void Update()
    {
        float baseHeight = GameManager.Instance.Score * GameManager.Instance.Denominator;
        Vector3 newPosition = new (0, _globalOffset + baseHeight, 0);

        if (transform.position.y < newPosition.y)
        {
            transform.position = newPosition;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        if (_camera) _camera.DisablePlayerTracking();
        _player.DisableInputs();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        float timer = 0f;
        
        if (!other.CompareTag("Player")) return;

        while (timer < 1)
        {
            timer += Time.deltaTime;
        }
        
        if (_player.transform.position.y < transform.position.y)
        {
            Debug.Log("GameOver");
            SceneLoader.Instance.LoadNextScene();
        }
    }
}
