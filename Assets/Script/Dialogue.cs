using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    /*[System.Serializable]
    public struct CharacterDialogue
    {
        public string characterName;
        public string line;
    }

    public TextMeshProUGUI characterNameText;  // Reference to the TextMeshProUGUI for the character name
    public TextMeshProUGUI dialogueText;  // Reference to the TextMeshProUGUI for the dialogue line

    public CharacterDialogue[] characterDialogues;  // Array of CharacterDialogue
    public float textSpeed;

    private int index;

    public void InitializeDialogue(CharacterDialogue[] dialogues, float speed)
    {
        characterDialogues = dialogues;
        textSpeed = speed;
        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0;
        dialogueText.text = string.Empty;
        characterNameText.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        characterNameText.text = characterDialogues[index].characterName;
        foreach (char c in characterDialogues[index].line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        // Wait for 5 seconds before moving to the next line
        yield return new WaitForSeconds(5f);
        NextLine();
    }

    void NextLine()
    {
        if (index < characterDialogues.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            StartCoroutine(HideDialogAfterDelay(5f)); // Hide after 5 seconds
        }
    }

    private IEnumerator HideDialogAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }*/
}
