using UnityEngine;

public static class ProgressManager
{
    private const string STARS_KEY = "LevelStars_";
    private const string UNLOCKED_KEY = "LevelUnlocked_";

    public static int GetStars(int levelIndex)
    {
        return PlayerPrefs.GetInt(STARS_KEY + levelIndex, 0);
    }

    public static void SaveStars(int levelIndex, int stars)
    {
        int currentStars = GetStars(levelIndex);
        if (stars > currentStars)
        {
            PlayerPrefs.SetInt(STARS_KEY + levelIndex, stars);
        }
    }

    public static void UnlockLevel(int levelIndex)
    {
        PlayerPrefs.SetInt(UNLOCKED_KEY + levelIndex, 1);
    }

    public static bool IsLevelUnlocked(int levelIndex)
    {
        if (levelIndex == 1) return true;
        return PlayerPrefs.GetInt(UNLOCKED_KEY + levelIndex, 0) == 1;
    }

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }
}