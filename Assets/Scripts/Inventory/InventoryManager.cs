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

    public int AddItem(string itemName, int quantity, GameObject gameObject, string itemDescription, int maxStackableAmount)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, gameObject, itemDescription, maxStackableAmount);

                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, gameObject, itemDescription, maxStackableAmount);
                }

                return leftOverItems;
            }
        }
        return quantity;
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
