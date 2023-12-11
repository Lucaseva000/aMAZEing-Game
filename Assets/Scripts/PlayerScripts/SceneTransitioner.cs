using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransitioner : MonoBehaviour
{

   
   [SerializeField] public LayerMask P;
    public string direction;
    public MoveDirectionSO M;
    public string targetScene;
    void Update()
    {
        if (CheckForPlayer())
        {
            M.setDirection(direction);
            SceneManager.LoadScene(targetScene);

        }
    }

    bool CheckForPlayer()
    {
        return Physics2D.OverlapBox(transform.position, transform.localScale / 2, 0, P);
    }


}
