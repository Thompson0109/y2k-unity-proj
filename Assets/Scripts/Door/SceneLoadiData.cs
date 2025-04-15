using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadData : MonoBehaviour
{
    public int sceneToLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}