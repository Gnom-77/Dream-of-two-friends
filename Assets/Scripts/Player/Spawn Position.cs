using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPosition;
    [SerializeField] private List<GameObject> _players;
    [SerializeField] private SavedNextSpawnPoint _nextSpawnPoint;
    //[SerializeField] private float _playerSpawnDistance;

    private void Start()
    {
        if (_nextSpawnPoint.DoorIndex < _spawnPosition.Count)
        {
            Debug.Log("Can transform");
            foreach (GameObject player in _players) 
            {
                player.transform.position = new Vector2(_spawnPosition[_nextSpawnPoint.DoorIndex].position.x, 
                    _spawnPosition[_nextSpawnPoint.DoorIndex].position.y);
            }
        }
        
    }
}
