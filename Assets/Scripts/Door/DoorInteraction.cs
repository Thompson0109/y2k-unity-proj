using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public FadeOutManager fadeOutManager;

    public GameObject Instruction;
    private GameObject currentDoor;
    private Collider colliderObj;
    public string scenename;

    public Vector3 newPlayerPosistion;
    private Transform player;

    void Start()
    {
        Instruction.SetActive(false);
    }

    void OnTriggerEnter(Collider collision)
    {
        colliderObj = collision;

        if (collision.transform.tag == "Player")
        {
            player = collision.transform;
            Instruction.SetActive(true);
            currentDoor = transform.parent.gameObject;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            Instruction.SetActive(false);
            currentDoor = null; 
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentDoor != null)
        {
            HandleDoorInteraction(currentDoor);
        }
    }

    void HandleDoorInteraction(GameObject door)
    {
        Instruction.SetActive(false);

        if (door.tag == "SceneLoadDoor")
        {
            StartCoroutine(SceneLoadDoor(door));
        }
        else if (door.tag == "AnimatedDoor")
        {
            AnimateDoor(door);
        }
        else
        {
            Debug.LogWarning(door.name + " has an unhandled tag: " + door.tag);
        }
    }

    IEnumerator SceneLoadDoor(GameObject door)
    {
        if (colliderObj.CompareTag("Player"))
        {
            yield return StartCoroutine(fadeOutManager.FadeOut());

            SceneManager.LoadScene(scenename);
            player.position = newPlayerPosistion;

            yield return StartCoroutine(fadeOutManager.FadeIn());
        }
        currentDoor = null;
    }
    void AnimateDoor(GameObject door)
    {
        Animator doorAnimator = door.GetComponentInChildren<Animator>();
        AudioSource[] allAudioSources = door.GetComponentsInChildren<AudioSource>();

        AudioSource doorOpeningSFX = allAudioSources.FirstOrDefault(source =>
            source.gameObject.name.Contains("DoorOpeningSFX"));
        AudioSource doorClosingSFX = allAudioSources.FirstOrDefault(source =>
            source.gameObject.name.Contains("DoorClosingSFX"));


        AnimationDoorData animateDoorComponent = door.GetComponent<AnimationDoorData>();
        if (animateDoorComponent == null)
        {
            animateDoorComponent = door.AddComponent<AnimationDoorData>();
        }

        animateDoorComponent.doorAnimator = doorAnimator;
        animateDoorComponent.doorOpenSound = doorOpeningSFX;
        animateDoorComponent.doorCloseSound = doorClosingSFX;

        animateDoorComponent.AnimateDoor();

        currentDoor = null;
    }
}