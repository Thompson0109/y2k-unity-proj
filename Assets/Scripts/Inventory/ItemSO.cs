using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int itemHungerReplenishAmount;

    public StatToChange statToChange = new StatToChange();

    public bool UseItem()
    {
        if (statToChange == StatToChange.wealth)
        {

        }
        if (statToChange == StatToChange.hunger)
        {
            Hunger playerHunger = GameObject.Find("Player").GetComponent<Hunger>();
            if(playerHunger.hunger == playerHunger.maxHunger)
            {
                return false;
            }
            playerHunger.HungerModifier(itemHungerReplenishAmount);
            return true;
        }
        if (statToChange == StatToChange.tiredness)
        {

        }
        return false;

    }
    public enum StatToChange
    {
        none,
        wealth,
        hunger,
        tiredness
    };

}
