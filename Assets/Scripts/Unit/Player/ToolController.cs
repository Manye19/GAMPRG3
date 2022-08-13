using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolController : MonoBehaviour
{
    private bool canUse = true;

    public ToolUsedEvent onToolUsedEvent = new ToolUsedEvent();
    public float staminaCost;

    private PlayerMovement movementScript;
    public Transform interactorPivot;
    public Transform interactor;
    public GameObject equippedObject;
    private string equippedObjectName;
    private GameObject swordGameObject;
    public Transform toolSpawnPoint;
    public InventoryManager inventoryScript;

    public float interactorRadius= 0.3f;
    public float thrust;
    public float knockBackTime;
        
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
        onToolUsedEvent.AddListener(PlayerManager.instance.playerStamina.ModifyStamina);
    }    

    // Update is called once per frame
    void Update()
    {
        RotateInteractorPivot();

        if(Input.GetMouseButtonDown(0))
        {
            if (equippedObject != null)
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
                swordGameObject = SetCurrentObject(4);
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
            UIManager.instance.inventoryUI.SetActive(!UIManager.instance.inventoryUI.activeSelf);
        }
    }

    public void CanUseUpdate(bool p_bool)
    {
        canUse = p_bool;
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
        if (canUse)
        {
            if (!equippedObject.Equals(swordGameObject))
            {
                onToolUsedEvent.Invoke(-staminaCost);
            }

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
                    if (equippedObject.CompareTag("Seeds"))
                    {
                        ItemData foundStackableItem = InventoryManager.instance.GetStackableItem(equippedObjectName);
                        GameObject foundUnstackableItem = InventoryManager.instance.GetUnstackableItem(equippedObjectName);
                        InventoryManager.instance.RemoveItem(foundUnstackableItem, foundStackableItem.so_Item);
                    }
                }
                if (equippedObject.Equals(swordGameObject))
                {
                    if (obj.gameObject.CompareTag("Enemy"))
                    {
                        Health health = obj.GetComponent<Health>();
                        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                        rb.isKinematic = false;
                        Vector2 difference = (rb.transform.position - transform.position);
                        difference = difference.normalized * thrust;
                        rb.AddForce(difference, ForceMode2D.Impulse);
                        StartCoroutine(KnockbackTimeCoroutine(health, rb));
                    }
                }                        
            }
        }
    }

    private IEnumerator KnockbackTimeCoroutine(Health p_health, Rigidbody2D p_rb)
    {
        if (p_rb != null)
        {
            yield return new WaitForSeconds(knockBackTime);
            p_rb.velocity = Vector2.zero;
            p_rb.isKinematic = true;
        }
        p_health.ModifyHealth(-25f);
    }

    private GameObject SetCurrentObject(int position)
    {
        if(equippedObject != null)
        {
            Destroy(this.equippedObject);
        }

        if (inventoryScript.unstackableItems.Count > 0)
        {
            equippedObject = Instantiate(inventoryScript.unstackableItems[position], interactorPivot, true);
            equippedObjectName = equippedObject.GetComponent<Item>().itemName;
        }
        equippedObject.transform.position = toolSpawnPoint.position;
        equippedObject.transform.rotation = interactorPivot.rotation;
        return equippedObject;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(interactor.position, interactorRadius);
    }
}
