using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    private PlayerMovement movementScript;
    public Transform interactorPivot;
    public Transform interactor;
    public GameObject equippedObject;
    public Transform toolSpawnPoint;
    public Inventory inventoryScript;

    public float interactorRadius= 0.3f;
        
    private void Awake()
    {
        movementScript = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        SetCurrentObject(0);
    }

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
            if (inventoryScript.inventoryItems.Count >= 1)
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
            if (inventoryScript.inventoryItems.Count >= 2)
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
            if (inventoryScript.inventoryItems.Count >= 3)
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
            if (inventoryScript.inventoryItems.Count >= 4)
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
            if (inventoryScript.inventoryItems.Count >= 5)
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
            if (inventoryScript.inventoryItems.Count >= 6)
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
            if (inventoryScript.inventoryItems.Count >= 7)
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
            if (inventoryScript.inventoryItems.Count >= 8)
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
            if (inventoryScript.inventoryItems.Count >= 9)
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
            if (inventoryScript.inventoryItems.Count >= 10)
            {
                SetCurrentObject(9);
            }
            else
            {
                equippedObject = null;
            }
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
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(interactor.position, interactorRadius);

        foreach(Collider2D obj in hitObjects)
        {
            if(equippedObject != null)
            {
                BreakableObject objScript = obj.GetComponent<BreakableObject>();
                if(objScript)
                {
                    objScript.OnHit(this.equippedObject, interactor.transform);
                }
                /*else
                {
                    Debug.Log("No hit");
                }*/
            }
        }
    }

    void SetCurrentObject(int position)
    {
        if(equippedObject != null)
        {
            Destroy(this.equippedObject);
        }

        if (inventoryScript.inventoryItems.Count > 0)
        {
            equippedObject = Instantiate(inventoryScript.inventoryItems[position], interactorPivot, true);

        }
        equippedObject.transform.position = toolSpawnPoint.position;
        equippedObject.transform.rotation = interactorPivot.rotation;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(interactor.position, interactorRadius);
    }
}
