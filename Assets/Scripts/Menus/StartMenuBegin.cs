using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Main menu function that updates the player so that everything is reset
public class StartMenuBegin : MonoBehaviour
{

    public PlayerHealth P;
    public PlayerInventoryManagment I;
    public void StartGame()
    {
        SceneManager.LoadScene(1);//Scene Manager
        Time.timeScale = 1;
        I.setWorth(0);
        P.setHealth(P.getMaxHealth());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
