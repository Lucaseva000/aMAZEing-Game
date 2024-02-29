using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Move Direction")]
public class MoveDirectionSO : ScriptableObject
{
    public string moveDirection;

    public void SetDirection(string s)
    {
        moveDirection = s;
    }

    public string getDirection()
    {
        return moveDirection;
    }
  
}
