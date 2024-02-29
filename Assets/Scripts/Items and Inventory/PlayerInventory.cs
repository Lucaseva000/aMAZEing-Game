using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class PlayerInventory : MonoBehaviour
{
    public PlayerInventoryManagment inventory;
    public TextMeshProUGUI coinText;
    public int selectedSlot;
    private PlayerControler playerControls;
    public GameObject hotbar;

    public void Awake()
    {
        playerControls = new PlayerControler();
    }
    void OnEnable()
    {
        playerControls.Enable();
    }
    void OnDisable()
    {
        playerControls.Disable();
    }
    public void Update()
    {
        setCoinText();
        setSlotInput();
       
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log(other.tag);
        //If collides with coin
        if (other.CompareTag("Coin"))
        {
            inventory.setWorth(other.GetComponent<CoinInfo>().Amount + inventory.getWorth()) ;
            Destroy(other.gameObject);
        }

        //if collides with item
        if (other.CompareTag("Item"))
        {
            if (inventory.AddItem(other.GetComponent<ItemInstanceHolder>().getItem()))
            {
                Destroy(other.gameObject);
            }
        }
    }
    //Reset the Coin Text to change amount
    public void setCoinText()
    {
        coinText.text = inventory.getWorth().ToString();
    }

    public void setSlotInput()
    {
        playerControls.Land.MenuSlotOne.started += SlotOne;
        playerControls.Land.MenuSlotTwo.started += SlotTwo;
        playerControls.Land.MenuSlotThree.started += SlotThree;
    }
    public void SlotOne(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            selectedSlot = 0;
        }
    }

    public void SlotTwo(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            selectedSlot = 1;
        }
    }

    public void SlotThree(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            selectedSlot = 2;
        }
    }

}
