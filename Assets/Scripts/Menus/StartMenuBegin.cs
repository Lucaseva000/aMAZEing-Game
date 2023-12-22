using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Main menu function that updates the player so that everything is reset
public class StartMenuBegin : MonoBehaviour
{

    public PlayerHealth P;
<<<<<<< HEAD
    public MoveDirectionSO d;
=======
    public PlayerInventoryManagment I;
>>>>>>> b7a9fade643cf1c870a6fe1577864d12d848173d
    public void StartGame()
    {
        SceneManager.LoadScene(1);//Scene Manager
        Time.timeScale = 1;
        I.setWorth(0);
        P.setHealth(P.getMaxHealth());
        d.setDirection("left");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
