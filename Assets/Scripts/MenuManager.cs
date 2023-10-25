using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Unity Engine.SceneManagment is super important. That is waht allows us to be able to swithc between the scenes
public class MenuManager : MonoBehaviour
{
    // The int that determines what scene is loaded when beginning. Scene numbers are under file in buildscene
    public int gameStartScene;

    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);//Scene Manager
    }

    public void QuitGame()
    {
        Application.Quit();//Exits the game (Only works in a built version)
    }

}
