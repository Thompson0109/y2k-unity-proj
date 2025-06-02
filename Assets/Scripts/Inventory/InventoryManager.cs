using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public ItemDatabase itemDatabase; // Drag in your ItemDatabase ScriptableObject in Inspector

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameObject GetPrefabForItem(ItemSO item)
    {
        return itemDatabase != null ? itemDatabase.GetPrefab(item) : null;
    }
    [System.Serializable]
    public class InventoryItem
    {
        public ItemSO itemSO;
        public int quantity;
    }

    public List<InventoryItem> inventory = new List<InventoryItem>();

    public GameObject inventoryMenu;
    public bool menueActivated;
    public ItemSlot[] itemSlot;

    public ItemSO[] itemSOs;
    private void Start()
    {
        menueActivated = false;
        RebuildInventorysUI();
    }
    private void RebuildInventorysUI()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].ClearSlot();
        }

        for (int i = 0; i < inventory.Count && i < itemSlot.Length; i++)
        {
            itemSlot[i].SetItem(inventory[i].itemSO, inventory[i].quantity);
        }
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
        // Update persistent inventory list
        InventoryItem existing = inventory.Find(i => i.itemSO == item);
        if (existing != null)
        {
            existing.quantity += quantity;
        }
        else
        {
            inventory.Add(new InventoryItem { itemSO = item, quantity = quantity });
        }

        // Update UI slots
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i] == null)
                continue; 

            int leftover = itemSlot[i].AddItem(item, quantity, gameObject);
            if (leftover < quantity)
            {
                if (leftover > 0)
                    return AddItem(item, leftover, gameObject);
                return 0;
            }
        }

        return quantity;
    }

    public void RemoveItem(ItemSO item)
    {
        InventoryItem existing = inventory.Find(i => i.itemSO == item);
        if (existing != null)
        {
            existing.quantity--;
            if (existing.quantity <= 0)
            {
                inventory.Remove(existing);
            }
        }
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
