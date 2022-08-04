using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public ItemScriptableObject itemScriptableObject;
    public GameObject itemPrefab;
    public string itemName;
    public Sprite UISprite;
    public bool isStackable;
    private float timer;
    private Transform playerTransform;

    private void Start()
    {
        timer = 2f;
    }

    private void OnEnable()
    {
        playerTransform = PlayerManager.instance.playerTransform;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Magnetize();
        }
    }

    private void Magnetize()
    {
        if (Vector3.Distance(transform.position, PlayerManager.instance.playerTransform.position) > 1)
        {
            Vector3 playerPoint = Vector3.MoveTowards(transform.position, PlayerManager.instance.playerTransform.position + new Vector3(0, 0, 0), 40f * Time.deltaTime);
            transform.position = (playerPoint);
        }
        else
        {
            timer = 2f;
            InventoryManager.instance.onAddItemEvent.Invoke(itemPrefab, itemScriptableObject, itemName, 1);
            Destroy(gameObject);
        }
    }
}
