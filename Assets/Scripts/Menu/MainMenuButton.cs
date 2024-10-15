using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private SavedNextSpawnPoint _nextSpawnPoint;
    public void NewGame()
    {
        _nextSpawnPoint.DoorIndex = 0;
        SceneManager.LoadScene("LvlOne");
    }

    public void ContinueGame()
    {
        Debug.Log("Is Continue");
    }

    public void Options()
    {
        Debug.Log("Is Options");
    }

    public void Exit()
    {
        Debug.Log("Is Quit");
        Application.Quit();
    }
}
