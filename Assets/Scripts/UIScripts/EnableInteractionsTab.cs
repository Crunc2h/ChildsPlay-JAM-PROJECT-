using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableInteractionsTab : MonoBehaviour
{
    private Inventory _inventory;
    private Color normalButtonColorHexadecimal = Color.black;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenInteractionTab()
    {
        CheckAndCloseIfAnyOpen();
        var parentSlot = transform.parent;
        var interactionTab = parentSlot.transform.GetChild(0);
        interactionTab.gameObject.SetActive(true);
        var itemReference = parentSlot.GetComponent<ItemReference>().currentItemReference;
        if(!itemReference.Usable)
        {
            var button = interactionTab.transform.GetChild(0).gameObject.GetComponent<Button>();
            button.interactable = false;
            button.image.color = normalButtonColorHexadecimal;
        }
        if(!itemReference.Interactable)
        {
            var button = interactionTab.transform.GetChild(1).gameObject.GetComponent<Button>();
            button.interactable = false;
            button.image.color = normalButtonColorHexadecimal;
        }
    }

    private void CheckAndCloseIfAnyOpen()
    {
        foreach(var slot in _inventory.slots)
        {
            var currentSlot = transform.parent;
            if(slot != currentSlot)
            {
                if(slot.transform.GetChild(0).gameObject.activeSelf)
                {
                    slot.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
}
