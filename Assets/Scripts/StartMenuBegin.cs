using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenuBegin : MonoBehaviour
{
    public PlayerHealth P;
    public void StartGame()
    {
        SceneManager.LoadScene(1);//Scene Manager
        Time.timeScale = 1;
        P.SetPlayerHealth(P.maxHealth);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
