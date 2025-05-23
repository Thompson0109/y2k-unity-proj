using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //=====ITEM DATA====//
    public string itemName;
    public int quantity;
    public GameObject gameItemObject;
    public bool isFull;
    public string itemDescription;

    //=====ITEM SLOT====//
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    public GameObject selectedShader;  
    public bool isSelected;

    private InventoryManager inventoryManager;

    //=====ITEM DESCRIPTION SLOT ====//
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    public void AddItem(string itemName, int quantity, GameObject gameObject, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.gameItemObject = gameObject;
        this.itemDescription = itemDescription;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        addImage();
    }

   private void addImage() {
        Image sourceImage = gameItemObject.GetComponent<Image>();

        if (sourceImage != null && sourceImage.sprite != null)
        {
            itemImage.sprite = sourceImage.sprite;
            itemImage.enabled = true; 
        }
        else
        {
            Debug.LogWarning("Item GameObject '" + gameItemObject.name + "' is missing an Image component or its sprite is null.");
            itemImage.enabled = false; 
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnRightClick()
    {
        selectedShader.SetActive(false);
        isSelected = true;
    }

    private void OnLeftClick()
    {
       inventoryManager.DeselectAllSlots();
   
       selectedShader.SetActive(true);
       isSelected = true;

       itemDescriptionNameText.text = itemName;
       itemDescriptionText.text = itemDescription;
       itemDescriptionImage.sprite = itemImage.sprite;

    }
}
