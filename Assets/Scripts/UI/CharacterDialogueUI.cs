using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterDialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject characterDialogueFrame;
    [SerializeField] private Image avatarImage;
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text dialogueText;

    private DialogueScriptableObject currentDialogueScriptableObject;

    public static CharacterSpokenToEvent onCharacterSpokenToEvent = new CharacterSpokenToEvent();
    public static CharacterLeaveEvent onCharacterLeaveEvent = new CharacterLeaveEvent();

    private void Awake()
    {
        onCharacterSpokenToEvent.AddListener(OnCharacterSpokenTo);
        onCharacterLeaveEvent.AddListener(OnCloseCharacterDialogueUI);
    }

    private void OnDestroy()
    {
        onCharacterSpokenToEvent.RemoveListener(OnCharacterSpokenTo);
        onCharacterLeaveEvent.RemoveListener(OnCloseCharacterDialogueUI);
    }

    public void OnCharacterSpokenTo(DialogueScriptableObject p_dialogueScriptableObject)
    {
        currentDialogueScriptableObject = p_dialogueScriptableObject;
        OnOpenCharacterDialogueUI();
    }
    
    public void OnOpenCharacterDialogueUI()
    {
        Dialogue currentDialogue = currentDialogueScriptableObject.dialogues[Random.Range(0, currentDialogueScriptableObject.dialogues.Count)];
        avatarImage.sprite = currentDialogue.characterScriptableObject.avatar;
        characterNameText.text = currentDialogue.characterScriptableObject.name;
        dialogueText.text = currentDialogue.words;
        characterDialogueFrame.SetActive(true);
    }

    public void OnCloseCharacterDialogueUI()
    {
        characterDialogueFrame.SetActive(false);
    }
}
