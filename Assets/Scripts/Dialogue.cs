using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]

public class Dialogue : ScriptableObject
{
    public string[] dialogueLines;
    public bool[] autoProgessLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpped = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    
}
