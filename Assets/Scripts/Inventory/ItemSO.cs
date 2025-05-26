using System;
using UnityEngine;
[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int itemHungerReplenishAmount;

    public StatToChange statToChange = new StatToChange();

    public void UseItem()
    {
        if (statToChange == StatToChange.wealth)
        {

        }
        if (statToChange == StatToChange.hunger)
        {
            GameObject.Find("Player").GetComponent<Hunger>().HungerModifier(itemHungerReplenishAmount);
        }
        if (statToChange == StatToChange.tiredness)
        {

        }

    }
    public enum StatToChange
    {
        none,
        wealth,
        hunger,
        tiredness
    };

}
