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
            inventoryManager.AddItem(itemName, quantity, gameObject, itemDescription);
            Destroy(gameObject);
        }
    }
}
