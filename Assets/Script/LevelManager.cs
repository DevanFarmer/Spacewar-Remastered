using UnityEngine.SceneManagement;
using UnityEngine;

public static class LevelManager
{
    public static void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex + 1 > SceneManager.sceneCount)
        {
            Debug.LogError("No more scenes to load!");
            return;
        }
        
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
