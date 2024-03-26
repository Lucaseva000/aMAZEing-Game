using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject AttackArea;
    private bool attacking;
    public float timeToAttack = 0.25f;
    public float timer = 0f;
    private PlayerControler playerControls;

    // The damage only applies on the first frame of the attack
    public void Awake()
    {
        playerControls = new PlayerControler();
    }
    void OnEnable()
    {
        playerControls.Enable();
    }

    void Update()
    {

        if (!attacking)
        {
            playerControls.Land.Attack.started += Attack;

        }


        if (attacking)
        {
            timer += Time.deltaTime;
            if(timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                AttackArea.SetActive(attacking);
            }
        }
    }
    private void Attack(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Started)
        {
            attacking = true;
            AttackArea.SetActive(true);
            AttackArea.GetComponent<AttackArea>().Attack();
        }
    }
}
