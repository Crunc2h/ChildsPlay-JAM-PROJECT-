using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UseOrInteract : MonoBehaviour
{
    public void OnUserClick()
    {
        var parentSlot = transform.parent.transform.parent.gameObject;
        var itemReference = transform.GetComponentInParent<ItemReference>(false);
        var itemIconObject = parentSlot.transform.GetChild(1).gameObject;
        var closeButton = transform.parent.transform.GetChild(3).gameObject;
        
        if(gameObject.tag == "UseButton")
        {
            itemReference.currentItemReference.Use(itemIconObject);
            closeButton.GetComponent<CloseTab>().CloseTabFunction();
        }
        else if(gameObject.tag == "InteractButton")
        {
            itemReference.currentItemReference.Interact(parentSlot);
        }
        else if(gameObject.tag == "DropButton")
        {
            var itemIcon = parentSlot.transform.GetChild(1).gameObject;
            var interactionTab = transform.parent.gameObject;
            var inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

            //Slotu boşalt
            EmptyInventorySlot(parentSlot, inventory);

            //item prefabını player pozisyonuna getir ve aktive et
            SpawnItemPrefab(parentSlot);

            //ikonu sil ve interaksyon pop-upunu kapa
            Destroy(itemIcon);
            interactionTab.SetActive(false);
        }
    }

    private static void EmptyInventorySlot(GameObject parentSlot, Inventory inventory)
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i] == parentSlot)
            {
                inventory.isFull[i] = false;
            }
        }
    }
    private void SpawnItemPrefab(GameObject parentSlot)
    {
        parentSlot.GetComponent<ItemReference>().itemPrefab.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,
                        GameObject.FindGameObjectWithTag("Player").transform.position.y, transform.position.z);
        parentSlot.GetComponent<ItemReference>().itemPrefab.SetActive(true);
    }
}
