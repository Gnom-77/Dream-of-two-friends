using UnityEngine;

public class ShowMenuInGame : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject[] _atherMenus;


    private bool _isActive;
    void Start()
    {
        CloseMenu();
    }

    void Update()
    {
        if (!_isActive && Input.GetKeyDown(KeyCode.Escape) && !DialogueManager.GetInstance().GetDialogueIsPlaying())
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
        Time.timeScale = 0.0f;

    }

    public void CloseMenu()
    {
        _mainMenu.SetActive(false);
        foreach (var item in _atherMenus)
        {
            if(item.activeSelf)
            { 
                item.SetActive(false); 
            }
        }

        _isActive = false;
        Time.timeScale = 1.0f;
    }

}
