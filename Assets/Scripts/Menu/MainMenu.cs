using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject MainMenuUI;
    public GameObject SettingsUI;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }
    public void ToggleSetting()
    {        
        MainMenuUI.SetActive(!MainMenuUI.activeSelf);
        SettingsUI.SetActive(!SettingsUI.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
