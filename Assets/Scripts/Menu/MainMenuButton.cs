using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private SavedNextSpawnPoint _nextSpawnPoint;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _controllMenu;

    public void NewGame()
    {
        _nextSpawnPoint.DoorIndex = 0;
        SceneManager.LoadScene("LvlOne");
    }

    public void ContinueGame()
    {
        Debug.Log("Is Continue");
    }


    public void MainMenuFromSettings()
    {
        _settingsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void SettingsFromMainMenu()
    {
        _settingsMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void ControllFromSettings()
    {
        _controllMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    public void SettingsFromControll()
    {
        _settingsMenu.SetActive(true);
        _controllMenu.SetActive(false);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Debug.Log("Is Quit");
        Application.Quit();
    }
}
