using UnityEngine;

public class PlayMenuMusic : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlayMenuMusic();
    }
}