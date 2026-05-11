using UnityEngine;

public class PlayLevelMusic : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlayLevelMusic();
    }
}