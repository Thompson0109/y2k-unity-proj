using UnityEngine;
[CreateAssetMenu]
public class ScriptableObjects : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();

    public void UseItem()
    {
        if (statToChange == StatToChange.wealth)
        {

        }
        if (statToChange == StatToChange.hunger)
        {

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
