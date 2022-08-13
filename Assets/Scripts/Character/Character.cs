using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : InteractableObject
{
    public List<DialogueScriptableObject> currentDialogueOptions;
    private List<DialogueScriptableObject> dialogueOptions;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetDialogueOptions()
    {
        currentDialogueOptions = new List<DialogueScriptableObject>(dialogueOptions);
    }

    protected override void OnInteract()
    {
        
    }
}
