using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class SceneTransitioner : MonoBehaviour
{

   
   [SerializeField] public LayerMask P;
    public string direction;
    public MoveDirectionSO M;
    public string targetScene;
    public int y = 0;

    void Update()
    {
        if (CheckForPlayer() && Input.GetKeyDown(KeyCode.E))
        {
            M.setDirection(direction);
            SceneManager.LoadScene(targetScene);

        }
    }

    bool CheckForPlayer()
    {
        return Physics2D.OverlapBox(transform.position + new Vector3(0, y, 0) , transform.localScale / 2, 0, P);
    }


}
