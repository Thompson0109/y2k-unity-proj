using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO itemSO;
    public int quantity = 1;

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        int leftover = inventoryManager.AddItem(itemSO, quantity, gameObject);

        if (leftover <= 0)
            gameObject.SetActive(false);
        else
            quantity = leftover;
    }
}
