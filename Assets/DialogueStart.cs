using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueStart : MonoBehaviour
{
    public Dialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    public static event Action<bool> PausePlayer = delegate { };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dialogueData == null)
            return;
        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        PausePlayer.Invoke(true);
        isDialogueActive = true;
        dialogueIndex = 0;

        dialoguePanel.SetActive(true);

        StartCoroutine(TypeLine());
    }
    void NextLine()
    {
        if (isTyping)
        {
            //Skip typing animation and show the full line
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            //If another line, type next line
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpped);
        }

        isTyping = false;

        if(dialogueData.autoProgessLines.Length > dialogueIndex && dialogueData.autoProgessLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }
    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        Destroy(gameObject);
        PausePlayer.Invoke(false);
    }
}