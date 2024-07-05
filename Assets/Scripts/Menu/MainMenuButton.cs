using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void NewGame()
    {
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
