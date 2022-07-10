using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToUI(Sprite UISprite)
    {
        foreach(Transform slot in this.gameObject.transform)
        {
            Image imageComponent = slot.GetChild(0).GetComponent<Image>();

            if(!imageComponent.enabled)
            {
                imageComponent.enabled = true;
                imageComponent.sprite = UISprite;

                break;
            }
        }
    }
}
