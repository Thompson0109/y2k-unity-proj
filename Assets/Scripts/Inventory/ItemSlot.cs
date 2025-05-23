using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    //=====ITEM DATA====//
    public string itemName;
    public int quantity;
    public GameObject gameObject;
    public bool isFull;

    //=====ITEM SLOT====//
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    public void AddItem(string itemName, int quantity, GameObject gameObject)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.gameObject = gameObject;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        addImage();
    }

   private void addImage() {
        Image sourceImage = gameObject.GetComponent<Image>();

        if (sourceImage != null && sourceImage.sprite != null)
        {
            // Assign the sprite from the source Image to your item slot's Image
            itemImage.sprite = sourceImage.sprite;
            itemImage.enabled = true; // Ensure the Image is visible
        }
        else
        {
            Debug.LogWarning("Item GameObject '" + gameObject.name + "' is missing an Image component or its sprite is null.");
            itemImage.enabled = false; // Hide the image if there's nothing to display
        }
    }

}
