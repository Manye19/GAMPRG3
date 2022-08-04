using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableObject
{
    public static BedInteractedEvent onBedInteractedEvent = new BedInteractedEvent();
    
    public Transform spawnTransform;
    

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnInteract()
    {
        onBedInteractedEvent.Invoke();
    }
    
    public void OnClickYesButton()
    {
        OnInteract();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.instance.OpenBedUI(true);
        }
    }
}
