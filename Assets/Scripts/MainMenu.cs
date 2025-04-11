using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SwitchToScene(string traingame)
    {
        SceneManager.LoadScene(traingame);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
