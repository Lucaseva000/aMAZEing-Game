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
    public int selectedSlot = 0;
    private PlayerControler playerControls;
    public GameObject hotbar;
    public List<GameObject> itemSlots = new();


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
    public void Start()
    {
        itemSlots.Add(hotbar.transform.Find("ItemSlotOne").gameObject);
        itemSlots.Add(hotbar.transform.Find("ItemSlotTwo").gameObject);
        itemSlots.Add(hotbar.transform.Find("ItemSlotThree").gameObject);
    }
    public void Update()
    {

        setCoinText();
        setSlotInput();
        slotUpdate();
        playerControls.Land.DropItem.started += ItemDrop;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

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

    public void ItemDrop(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if(inventory.items.Count > 0)
            {
                inventory.removeItem(selectedSlot);
            }
        }
    }

    public void slotUpdate()
    {

        enableSlot();

        for (int i = 0; i < itemSlots.Count - inventory.items.Count; i++)
        {
            itemSlots[itemSlots.Count - i - 1].transform.Find("ItemImage").gameObject.SetActive(false);
        }

        for (int i = 0; i < inventory.items.Count; i++)
        {
            
            itemSlots[i].transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite = inventory.items[i].itemType.icon;
            itemSlots[i].transform.Find("ItemImage").gameObject.SetActive(true);
        }


    }

    public void enableSlot()
    {
        for(int i = 0; i < itemSlots.Count; i++)
        {
            if(selectedSlot == i)
            {
                itemSlots[i].transform.Find("LightUp").gameObject.SetActive(true);
            }
            else
            {
                itemSlots[i].transform.Find("LightUp").gameObject.SetActive(false);
            }
        }
    }


}
