using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menueActivated;

    private void Start()
    {
        menueActivated = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory") && menueActivated)
        {
            InventoryMenu.SetActive(false);
            menueActivated = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menueActivated)
        {
            InventoryMenu.SetActive(true);
            menueActivated = true;
        }
    }

    public void AddItem(string itemName, int quantity, GameObject gameObject)
    {
        Debug.Log("itemName = " + itemName + "quantity = " + quantity + "gameObject" + gameObject);

    }
}
