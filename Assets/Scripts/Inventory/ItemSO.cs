using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;
    [TextArea]
    public string description;
    public Sprite icon;
    public GameObject prefab;
    public int maxStackableAmount = 1;

    [Header("Effect")]
    public StatToChange statToChange = StatToChange.none;
    public int effectAmount;

    public enum StatToChange
    {
        none,
        wealth,
        hunger,
        tiredness
    }

    public bool UseItem()
    {
        switch (statToChange)
        {
            case StatToChange.hunger:
                Hunger playerHunger = GameObject.FindWithTag("Player")?.GetComponent<Hunger>();
                if (playerHunger == null || playerHunger.hunger == playerHunger.maxHunger)
                    return false;

                playerHunger.HungerModifier(effectAmount);
                return true;

            case StatToChange.wealth:
                return true;

            case StatToChange.tiredness:
                return true;

            default:
                return false;
        }
    }
}
