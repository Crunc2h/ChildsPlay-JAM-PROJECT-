using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UseOrInteract : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUserClick()
    {
        var itemReference = transform.GetComponentInParent<ItemReference>(false);
        if(gameObject.tag == "UseButton")
        {
            itemReference.currentItemReference.Use();
        }
        else if(gameObject.tag == "InteractButton")
        {
            itemReference.currentItemReference.Interact();
        }
        else if(gameObject.tag == "DropButton")
        {
            var parentSlot = transform.parent.transform.parent.gameObject;
            var itemIcon = parentSlot.transform.GetChild(1).gameObject;
            var inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            var interactionTab = transform.parent.gameObject;
            for(int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.slots[i] == parentSlot)
                {
                    inventory.isFull[i] = false;
                }
            }
            parentSlot.GetComponent<ItemReference>().itemPrefab.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,
                GameObject.FindGameObjectWithTag("Player").transform.position.y, transform.position.z);
            parentSlot.GetComponent<ItemReference>().itemPrefab.SetActive(true);
            Destroy(itemIcon);
            interactionTab.SetActive(false);
        }
    }
}
