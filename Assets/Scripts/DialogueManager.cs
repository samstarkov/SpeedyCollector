using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speaker;
        [TextArea]
        public string text;
    }

    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public List<DialogueLine> dialogues;
    public string nextScene;

    private int index = 0;

    void Start()
    {
        if (dialogues.Count > 0)
            ShowDialogue();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            index++;
            if (index < dialogues.Count)
                ShowDialogue();
            else
                SceneManager.LoadScene(nextScene);
        }
    }

    void ShowDialogue()
    {
        speakerText.text = dialogues[index].speaker;
        dialogueText.text = dialogues[index].text;
    }
}