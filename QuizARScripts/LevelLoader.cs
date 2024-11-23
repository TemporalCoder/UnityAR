using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class LevelLoader : MonoBehaviour
{
    public void Start()
    {
        
    }

    public void loadLevel(string levelName)
    {
        Debug.Log("Clicked " + levelName);
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
}
