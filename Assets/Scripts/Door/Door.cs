using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _openDoorSprite;
    [SerializeField] private string _nextSceneName;
    [SerializeField] private int _doorIndex;
    [SerializeField] private SavedNextSpawnPoint _nextSpawnPoint;

    private bool _playerOne;
    private bool _playerTwo;
    void Start()
    {
        _openDoorSprite.enabled = false;
    }

    private void Update()
    {
        ChangedScene();
        ButtonIsActive();
        ButtonIsNotActive();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerOne = true;
            //Debug.Log("Is Enter");
        }
        if (collision.CompareTag("Player"))
        {
            _playerTwo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerOne = false;
            //Debug.Log("Is Exit");
        }
        if (collision.CompareTag("Player"))
        {
            _playerTwo = false;
        }
    }

    private void ButtonIsActive()
    {
        if (_openDoorSprite.isVisible == false && (_playerOne || _playerTwo))
        {
            _openDoorSprite.enabled = true;
        }
    }
    private void ButtonIsNotActive()
    {
        if (_openDoorSprite.isVisible == true && !_playerOne && !_playerTwo)
        {
            _openDoorSprite.enabled = false;
        }
    }

    private void ChangedScene()
    {
        if (_playerOne && Input.GetKeyDown(KeyCode.K))
        {
            _nextSpawnPoint.DoorIndex = _doorIndex;
            SceneManager.LoadScene(_nextSceneName);
        }
        if (_playerTwo && Input.GetKeyDown(KeyCode.Keypad3))
        {
            _nextSpawnPoint.DoorIndex = _doorIndex;
            SceneManager.LoadScene(_nextSceneName);
        }
    }


}
