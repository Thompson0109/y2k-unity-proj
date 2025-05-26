using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private GameObject itemObj;

    [SerializeField]
    private int maxStackableAmount;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    private Collider colliderObj;
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }


    void OnTriggerEnter(Collider collision)
    {
        colliderObj = collision;

        if (collision.transform.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, gameObject, itemDescription, maxStackableAmount);
            if(leftOverItems <= 0)
            Destroy(gameObject);
            else
                quantity = leftOverItems;
        }
    }
}
