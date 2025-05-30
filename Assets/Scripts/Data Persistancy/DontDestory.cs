using UnityEngine;

public class DontDestory : MonoBehaviour
{

    private static GameObject[] persistentObjects = new GameObject[3];
    public int objectIndex;


    private void Awake()
    {
        if (persistentObjects[objectIndex] == null)
        {
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        //only the object that matches the one in the list will persist
        //the other one we will destory
        else if (persistentObjects[objectIndex] != gameObject)
        {
            Destroy(gameObject);
        }

    }
}
