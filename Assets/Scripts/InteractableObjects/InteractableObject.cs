using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public InteractEvent onInteractEvent = new InteractEvent();

    public bool canInteract;
    protected virtual void OnEnable()
    {
        canInteract = true;
        onInteractEvent.AddListener(OnInteract);
    }

    protected virtual void OnDisable()
    {
        onInteractEvent.RemoveListener(OnInteract);
    }

    protected virtual void OnInteract()
    {

    }
}
