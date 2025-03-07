using UnityEngine;

public class PressKeyOpenDoor : MonoBehaviour
{
    public GameObject AnimeObject;
    public GameObject Instruction;
    public GameObject ThisTrigger;
    public AudioSource DoorOpenSound;
    public AudioSource DoorCloseSound; 

    public bool isOpen = false;
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
        if (Input.GetKeyDown(KeyCode.E) && Action) 
        {
            if (isOpen)
            {
                // Close the door
                AnimeObject.GetComponent<Animator>().Play("DoorClose");
                DoorOpenSound.Play();
                isOpen = false;
            }
            else
            {
                // Open the door
                AnimeObject.GetComponent<Animator>().Play("DoorOpen");
                DoorCloseSound.Play();
                isOpen = true;
            }

            Instruction.SetActive(false);
            Action = false;
        }
    }
}