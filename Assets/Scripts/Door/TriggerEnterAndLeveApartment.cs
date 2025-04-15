using UnityEngine;
using UnityEngine.SceneManagement;

public class PressKeyEnterApartmentA : MonoBehaviour
{  
    public GameObject currentDoor;
    public GameObject Instruction;
    public bool Action = false;

    void Start()
    {
        Instruction.SetActive(false);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            Instruction.SetActive(true);
            Action = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Instruction.SetActive(false);
        Action = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentDoor.name == "ApartmentDoor")
        {
            if (Action == true)
            {
                Instruction.SetActive(false);
                Action = false;
                SceneManager.LoadScene(1);
            }
        }
    }
}