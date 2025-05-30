using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    public bool menueActivated;
    public ItemSlot[] itemSlot;

    public ItemSO[] itemSOs;
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

    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                bool usable = itemSOs[i].UseItem();
                return usable;
            }
        }
        return false;
    }

    public int AddItem(ItemSO item, int quantity, GameObject gameObject)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            int leftover = itemSlot[i].AddItem(item, quantity, gameObject);
            if (leftover < quantity)
            {
                // some amount was added to this slot
                if (leftover > 0)
                    return AddItem(item, leftover, gameObject);

                return 0;
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
