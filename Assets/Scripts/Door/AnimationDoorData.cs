using UnityEngine;

public class AnimationDoorData : MonoBehaviour
{
    public Animator doorAnimator;
    public string openAnimationName = "DoorOpen";
    public string closeAnimationName = "DoorClose";
    public AudioSource doorOpenSound;
    public AudioSource doorCloseSound;
    public bool isOpen = false;

    public void AnimateDoor()
    {
        if (isOpen)
        {
            doorAnimator.Play(closeAnimationName);
            if (doorCloseSound != null)
            {
                doorCloseSound.Play();
            }
            isOpen = false;
        }
        else
        {
            doorAnimator.Play(openAnimationName);
            if (doorOpenSound != null)
            {
                doorOpenSound.Play();
            }
            isOpen = true;
        }
    }
}