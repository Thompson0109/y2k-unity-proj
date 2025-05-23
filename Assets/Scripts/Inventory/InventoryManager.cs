using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    private bool menueActivated;
    public ItemSlot[] itemSlot;
    private void Start()
    {
        menueActivated = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory") && menueActivated)
        {
            inventoryMenu.SetActive(false);
            menueActivated = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menueActivated)
        {
            inventoryMenu.SetActive(true);
            menueActivated = true;
        }
    }

    public void AddItem(string itemName, int quantity, GameObject gameObject)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, quantity, gameObject);
                return;
            }

        }
        Debug.Log("itemName = " + itemName + "quantity = " + quantity + "gameObject" + gameObject);
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].isSelected = false;
        }
    }
}
