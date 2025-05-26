using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public int hunger;
    public int maxHunger = 10;
    public Slider slider;

    [Tooltip("How many seconds it takes to lose 1 hunger point.")]
    public float hungerDecreaseInterval = 5f;

    private Coroutine _hungerCoroutine;


    private void Start()
    {
        hunger = maxHunger;
        slider.maxValue = maxHunger;
        slider.value = hunger;

        _hungerCoroutine = StartCoroutine(DecreaseHungerOverTime());
        Debug.Log("Hunger system started. Current hunger: " + hunger + "/" + maxHunger);
    }

    IEnumerator DecreaseHungerOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(hungerDecreaseInterval);

            if (hunger > 0)
            {
                hunger--;
                slider.value = hunger;
                Debug.Log("Hunger decreased. Current hunger: " + hunger + "/" + maxHunger);
            }
            else
            {
                Debug.Log("Hunger is at 0.");
            }
        }
    }

    //when this player eats something
    public void HungerModifier(int amount)
    {
        hunger += amount;
                slider.value = hunger;

        // Clamp the hunger value so it doesn't go below 0 or above maxHunger
        if (hunger > maxHunger)
        {
            hunger = maxHunger;
        }
        else if (hunger < 0)
        {
            hunger = 0;
        }
    }
    //when gameobject is disabled or deleted we stop the hunger coroutine
    private void OnDisable()
    {
        if (_hungerCoroutine != null)
        {
            StopCoroutine(_hungerCoroutine);
            _hungerCoroutine = null; // Clear the reference
            Debug.Log("Hunger decrease coroutine stopped due to object being disabled.");
        }
    }
}
