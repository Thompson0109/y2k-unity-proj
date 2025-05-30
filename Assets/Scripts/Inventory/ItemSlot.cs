using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;
using System.Collections.Generic;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("UI References")]
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private Sprite emptySlotSprite;
    [SerializeField] public GameObject selectedShader;

    [Header("Description UI")]
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    [Header("State")]
    public ItemSO itemSO;
    public int quantity;
    public bool isSelected;

    private InventoryManager inventoryManager;

    private List<GameObject> storedWorldItems = new List<GameObject>();
    private GameObject currentGameObject;
    public bool IsFull => itemSO != null && quantity >= itemSO.maxStackableAmount;
    public bool IsEmpty => itemSO == null || quantity <= 0;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        UpdateUI();
    }

    public int AddItem(ItemSO item, int amount, GameObject gameObject)
    {

        if (itemSO == null)
            itemSO = item;

        storedWorldItems.Add(gameObject);
        currentGameObject = gameObject;

        // if the slot is not empty but contains a different item, it gets rejected
        if (itemSO != null && itemSO != item)
            return amount;

        // calculates how many we can add
        int spaceLeft = itemSO.maxStackableAmount - quantity;
        int amountToAdd = Mathf.Min(spaceLeft, amount);

        quantity += amountToAdd;
        UpdateUI();

        // returns leftovers
        return amount - amountToAdd; 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            OnLeftClick();
        else if (eventData.button == PointerEventData.InputButton.Right)
            OnRightClick();
    }

    private void OnLeftClick()
    {
        if (isSelected)
        {
            bool used = inventoryManager.UseItem(itemSO.name);
            if (used)
            {
                quantity--;
                if (storedWorldItems.Count > 0)
                {
                    GameObject itemToDestroy = storedWorldItems[storedWorldItems.Count - 1];
                    storedWorldItems.RemoveAt(storedWorldItems.Count - 1);

                    GameObject.Destroy(itemToDestroy);
                }

                if  (quantity <= 0)
                    EmptySlot();

                UpdateUI();
            }
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            isSelected = true;
            selectedShader.SetActive(true);
            UpdateDescriptionUI();
        }
    }

    private void OnRightClick()
    {
        isSelected = true;
        selectedShader.SetActive(false);
        DropItem();
    }

    private void DropItem()
    {
        if (storedWorldItems.Count == 0)
        {
            Debug.LogWarning("No item instance to drop!");
            return;
        }

        GameObject itemToDrop = storedWorldItems[storedWorldItems.Count - 1];
        storedWorldItems.RemoveAt(storedWorldItems.Count - 1);

        Transform playerTransform = GameObject.FindWithTag("Player").transform;
        itemToDrop.transform.position = playerTransform.position + new Vector3(1f, 0, 0);
        itemToDrop.SetActive(true);

        quantity -= 1;
        quantityText.text = quantity.ToString();

        if (quantity <= 0)
            EmptySlot();
    }

    public void EmptySlot()
    {
        itemSO = null;
        quantity = 0;
        isSelected = false;
        selectedShader.SetActive(false);
        UpdateUI();
        ClearDescriptionUI();
    }

    private void UpdateUI()
    {
        if (itemSO != null)
        {
            itemImage.sprite = itemSO.icon;
            itemImage.enabled = true;
            quantityText.text = quantity.ToString();
            quantityText.enabled = quantity > 1;
        }
        else
        {
            itemImage.sprite = emptySlotSprite;
            itemImage.enabled = true;
            quantityText.enabled = false;
        }
    }

    private void UpdateDescriptionUI()
    {
        if (itemSO != null)
        {
            itemDescriptionImage.sprite = itemSO.icon;
            itemDescriptionNameText.text = itemSO.itemName;
            itemDescriptionText.text = itemSO.description;
        }
    }

    private void ClearDescriptionUI()
    {
        itemDescriptionImage.sprite = null;
        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
    }
}