using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private GameObject itemObj;

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
            inventoryManager.AddItem(itemName, quantity, gameObject);
            Destroy(gameObject);
        }
    }
}
