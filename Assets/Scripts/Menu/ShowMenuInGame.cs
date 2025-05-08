using UnityEngine;

public class ShowMenuInGame : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;

    private bool _isActive;
    void Start()
    {
        _mainMenu.SetActive(false);
        _isActive = false;
    }

    void Update()
    {
        if (!_isActive && Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
        else if (_isActive && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
    }
    public void OpenMenu()
    {
        _mainMenu.SetActive(true);
        _isActive = true;
    }

    public void CloseMenu()
    {
        _mainMenu.SetActive(false);
        _isActive = false;
    }

}
