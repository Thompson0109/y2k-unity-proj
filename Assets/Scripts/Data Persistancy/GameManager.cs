using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance;

    [Header("Persistent Objects")]
    public GameObject[] persistentObjects; 

    private void Awake()
    {
        if(Instance != null)
        {
            cleanUpAndDestroy();
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects(); 
        }
    }

    private void cleanUpAndDestroy()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        // destroys the duplicate GameManager (not the original one)
        Destroy(gameObject);
    }

    private void MarkPersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if(obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }
}
