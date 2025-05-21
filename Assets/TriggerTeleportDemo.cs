using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerTeleportDemo : MonoBehaviour
{
    public string scenename;
   
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(scenename);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);

        // Now it's safe to look for cameras
        Camera[] allCameras = Camera.allCameras;
        Debug.Log("Cameras found: " + allCameras.Length);

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
