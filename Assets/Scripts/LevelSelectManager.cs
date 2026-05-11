using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelectManager : MonoBehaviour
{
    public Button cutsceneButtonLeft;
    public Button cutsceneButtonRight;

    private string leftScenePage1 = "Cutscene_Intro";
    private string rightScenePage1 = "Cutscene_Mid";
    private string leftScenePage2 = "Cutscene_Mid";
    private string rightScenePage2 = "Cutscene_Ending";

    public Button[] levelButtons;
    public Button leftArrow;
    public Button rightArrow;

    public int levelsPerPage = 6;
    private int currentPage = 0;
    private int totalPages;

    void Start()
    {
        cutsceneButtonLeft.onClick.AddListener(() => LoadCutscene(currentPage == 0 ? leftScenePage1 : leftScenePage2));
        cutsceneButtonRight.onClick.AddListener(() => LoadCutscene(currentPage == 0 ? rightScenePage1 : rightScenePage2));
        totalPages = Mathf.CeilToInt((float)levelButtons.Length / levelsPerPage);

        leftArrow.onClick.AddListener(PrevPage);
        rightArrow.onClick.AddListener(NextPage);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            Button btn = levelButtons[i];

            if (ProgressManager.IsLevelUnlocked(levelIndex))
            {
                btn.interactable = true;
                btn.onClick.AddListener(() => LoadLevel(levelIndex));

                int stars = ProgressManager.GetStars(levelIndex);
                TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
                if (btnText != null)
                {
                    string starsString = "";
                    for (int s = 0; s < stars; s++) starsString += "*";
                    btnText.text = levelIndex + "\n" + starsString;
                }
            }
            else
            {
                btn.interactable = false;
                TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
                if (btnText != null)
                    btnText.text = levelIndex + "\nX";
            }
        }

        UpdatePage();
    }

    void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level_" + levelIndex);
    }

    void PrevPage()
    {
        currentPage--;
        if (currentPage < 0) currentPage = totalPages - 1;
        UpdatePage();
    }

    void NextPage()
    {
        currentPage++;
        if (currentPage >= totalPages) currentPage = 0;
        UpdatePage();
    }

    void UpdatePage()
    {
        int startIndex = currentPage * levelsPerPage;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i >= startIndex && i < startIndex + levelsPerPage)
                levelButtons[i].gameObject.SetActive(true);
            else
                levelButtons[i].gameObject.SetActive(false);
        }

        TextMeshProUGUI leftText = cutsceneButtonLeft.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI rightText = cutsceneButtonRight.GetComponentInChildren<TextMeshProUGUI>();

        if (currentPage == 0)
        {
            leftText.text = "Intro";
            rightText.text = "...";
        }
        else
        {
            leftText.text = "...";
            rightText.text = "Ending";
        }

        if (currentPage == 0)
        {
            cutsceneButtonLeft.interactable = true;

            bool passedLevel6 = ProgressManager.GetStars(6) >= 1;
            cutsceneButtonRight.interactable = passedLevel6;
        }
        else
        {
            bool passedLevel6 = ProgressManager.GetStars(6) >= 1;
            cutsceneButtonLeft.interactable = passedLevel6;

            bool passedLevel12 = ProgressManager.GetStars(12) >= 1;
            cutsceneButtonRight.interactable = passedLevel12;

            if (!passedLevel6) leftText.text = "???";
            if (!passedLevel12) rightText.text = "???";
        }
    }

    void LoadCutscene(string sceneName)
    {
        if (sceneName == "Cutscene_Mid" && !ProgressManager.IsLevelUnlocked(6))
        {
            Debug.Log("Катсцена Mid ещё не открыта");
            return;
        }
        if (sceneName == "Cutscene_Ending" && !ProgressManager.IsLevelUnlocked(13))
        {
            Debug.Log("Катсцена Ending ещё не открыта");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}