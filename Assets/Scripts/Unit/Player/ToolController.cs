using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    public ToolUsedEvent OnToolUsedEvent = new ToolUsedEvent();
    public float staminaCost;

    private PlayerMovement movementScript;
    public Transform interactorPivot;
    public Transform interactor;
    public GameObject equippedObject;
    public Transform toolSpawnPoint;
    public Inventory inventoryScript;

    public float interactorRadius= 0.3f;
        
    private void Awake()
    {
        movementScript = PlayerManager.instance.playerMovement;
    }

    private void Start()
    {
        SetCurrentObject(0);
    }

    private void OnEnable()
    {
        OnToolUsedEvent.AddListener(PlayerManager.instance.playerStamina.ModifyStamina);
    }

    //private void OnDisable()
    //{
    //    OnToolUsedEvent.RemoveListener(PlayerManager.instance.playerStamina.ModifyStamina);
    //}

    // Update is called once per frame
    void Update()
    {
        RotateInteractorPivot();

        if(Input.GetMouseButtonDown(0))
        {
            if(equippedObject != null)
            {
                equippedObject.GetComponent<Tools>().OnUse();

                ToolInteract();
            }
        }

        //For Switching Items
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (inventoryScript.unstackableItems.Count >= 1)
            {
                SetCurrentObject(0);
            }
            else
            {
                equippedObject = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (inventoryScript.unstackableItems.Count >= 2)
            {
                SetCurrentObject(1);
            }
            else
            {
                equippedObject = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (inventoryScript.unstackableItems.Count >= 3)
            {
                SetCurrentObject(2);
            }
            else
            {
                equippedObject = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (inventoryScript.unstackableItems.Count >= 4)
            {
                SetCurrentObject(3);
            }
            else
            {
                equippedObject = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (inventoryScript.unstackableItems.Count >= 5)
            {
                SetCurrentObject(4);
            }
            else
            {
                equippedObject = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (inventoryScript.unstackableItems.Count >= 6)
            {
                SetCurrentObject(5);
            }
            else
            {
                equippedObject = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (inventoryScript.unstackableItems.Count >= 7)
            {
                SetCurrentObject(6);
            }
            else
            {
                equippedObject = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (inventoryScript.unstackableItems.Count >= 8)
            {
                SetCurrentObject(7);
            }
            else
            {
                equippedObject = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (inventoryScript.unstackableItems.Count >= 9)
            {
                SetCurrentObject(8);
            }
            else
            {
                equippedObject = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (inventoryScript.unstackableItems.Count >= 10)
            {
                SetCurrentObject(9);
            }
            else
            {
                equippedObject = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            UIManager.instance.InventoryUI.SetActive(!UIManager.instance.InventoryUI.activeSelf);
        }
    }

    void RotateInteractorPivot()
    {
        if (movementScript.movement.x > 0)
        {
            interactorPivot.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (movementScript.movement.x < 0)
        {
            interactorPivot.localRotation = Quaternion.Euler(0, 0, -90);
        }
        if (movementScript.movement.y > 0)
        {
            interactorPivot.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (movementScript.movement.y < 0)
        {
            interactorPivot.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    

    void ToolInteract()
    {
        OnToolUsedEvent.Invoke(staminaCost);

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(interactor.position, interactorRadius);

        foreach (Collider2D obj in hitObjects)
        {
            if (equippedObject != null)
            {
                BreakableObject objScript = obj.GetComponent<BreakableObject>();
                if (objScript)
                {
                    objScript.OnHit(this.equippedObject, interactor.transform);
                }
            }
        }
    }

    //void ToolInteract()
    //{
    //    OnToolUsedEvent.Invoke(staminaCost);
    //    BreakableObject targetBreakableObject = GetBreakableObject();
    //    if (targetBreakableObject)
    //    {
    //        targetBreakableObject.onBreakableObjectHitEvent.Invoke(equippedObject, interactor.transform);
    //    }
    //}

    //public BreakableObject GetBreakableObject()
    //{
    //    Collider2D[] hitObjects = Physics2D.OverlapCircleAll(interactor.position, interactorRadius);
    //    foreach (Collider2D obj in hitObjects)
    //    {
    //        if (obj != null)
    //        {
    //            if (obj.TryGetComponent(out BreakableObject breakableObject))
    //            {
    //                if (breakableObject.TryGetComponent(out Health health))
    //                {
    //                    if (health.isAlive)
    //                    {
    //                        return breakableObject;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return null;
    //}

    void SetCurrentObject(int position)
    {
        if(equippedObject != null)
        {
            Destroy(this.equippedObject);
        }

        if (inventoryScript.unstackableItems.Count > 0)
        {
            equippedObject = Instantiate(inventoryScript.unstackableItems[position], interactorPivot, true);

        }
        equippedObject.transform.position = toolSpawnPoint.position;
        equippedObject.transform.rotation = interactorPivot.rotation;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(interactor.position, interactorRadius);
    }
}
