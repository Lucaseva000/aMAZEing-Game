using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
//Unity Engine.SceneManagment is super important. That is waht allows us to be able to swithc between the scenes
public class MenuManager : MonoBehaviour
{
    // The int that determines what scene is loaded when beginning. Scene numbers are under file in buildscene
    public int gameStartScene;

    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);//Scene Manager
        Time.timeScale = 1;

    }


}
