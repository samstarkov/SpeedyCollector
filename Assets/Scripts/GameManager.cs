using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject endPanel;
    public TextMeshProUGUI endTitleText;
    public TextMeshProUGUI starsText;
    public GameObject nextLevelButton;
    public GameObject restartButton;
    public GameObject menuButton;

    public float levelTime = 60f;
    public int star3Time = 30;
    public int star2Time = 45;

    public int totalItems = 0;
    private int collectedItems = 0;
    private float currentTime;
    private bool levelComplete = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        totalItems = GameObject.FindGameObjectsWithTag("Collectible").Length;
        currentTime = levelTime;
        endPanel.SetActive(false);
        UpdateUI();
    }

    void Update()
    {
        if (levelComplete) return;

        currentTime -= Time.deltaTime;
        UpdateUI();

        if (currentTime <= 0)
        {
            currentTime = 0;
            LevelFailed();
        }
    }

    public void AddScore()
    {
        collectedItems++;
        UpdateUI();

        if (collectedItems >= totalItems)
        {
            LevelComplete();
        }
    }

    void LevelComplete()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlayWinSound();

        levelComplete = true;
        endPanel.SetActive(true);
        endTitleText.text = "Level complete!";
        endTitleText.color = Color.green;

        int stars = CalculateStars();
        starsText.text = "Stars:\n" + stars + "/3";
        starsText.gameObject.SetActive(true);
        nextLevelButton.SetActive(true);
        restartButton.SetActive(true);

        int levelIndex = GetCurrentLevelIndex();
        ProgressManager.SaveStars(levelIndex, stars);

        if (stars >= 1)
        {
            ProgressManager.UnlockLevel(levelIndex + 1);
        }

        Debug.Log("œŒ¡≈ƒ¿! «‚∏Á‰: " + stars);
    }

    public void LevelFailed()
    {
        if (levelComplete) return;
        levelComplete = true;
        StartCoroutine(ShowFailPanel());
    }

    IEnumerator ShowFailPanel()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlayLoseSound();
        yield return new WaitForSeconds(0.1f);
        endPanel.SetActive(true);
        endTitleText.text = "Level failed!";
        endTitleText.color = Color.red;
        starsText.gameObject.SetActive(false);
        nextLevelButton.SetActive(false);
        restartButton.SetActive(true);
        menuButton.SetActive(true);
        Debug.Log("œŒ–¿∆≈Õ»≈! »„ÓÍ ÔÓ„Ë·!");
    }

    int CalculateStars()
    {
        float timeUsed = levelTime - currentTime;
        if (timeUsed <= star3Time) return 3;
        if (timeUsed <= star2Time) return 2;
        return 1;
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = collectedItems + "/" + totalItems;

        if (timerText != null)
        {
            int totalSeconds = Mathf.CeilToInt(currentTime);
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            timerText.text = minutes + ":" + seconds.ToString("00");
        }
    }

    int GetCurrentLevelIndex()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string numberPart = sceneName.Replace("Level_", "");
        int index;
        if (int.TryParse(numberPart, out index))
            return index;
        return 1;
    }

    public bool IsLevelComplete()
    {
        return levelComplete;
    }
}