using Unity.Cinemachine;
using UnityEngine;

public class MapTransation : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _mapBoundry;
    [SerializeField] private CinemachineConfiner2D _confiner;
    [SerializeField] private Transform[] _players;
    [SerializeField] private Transform _spawnPoinInNewLocation;

    private void Awake()
    {
        if (_confiner == null)
        {
            _confiner = FindFirstObjectByType<CinemachineConfiner2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player One") || collision.gameObject.CompareTag("Player Two"))
        {
            foreach (var player in _players)
            {
                player.transform.position = new Vector2(_spawnPoinInNewLocation.position.x, _spawnPoinInNewLocation.position.y);
            }
            _confiner.BoundingShape2D = _mapBoundry;
        }
    }
}
