using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanelManager : MonoBehaviour
{
    public void NextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int levelIndex;
        if (int.TryParse(currentScene.Replace("Level_", ""), out levelIndex))
        {
            string nextScene = "Level_" + (levelIndex + 1);
            if (Application.CanStreamedLevelBeLoaded(nextScene))
                SceneManager.LoadScene(nextScene);
            else
                SceneManager.LoadScene("LevelSelect");
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}