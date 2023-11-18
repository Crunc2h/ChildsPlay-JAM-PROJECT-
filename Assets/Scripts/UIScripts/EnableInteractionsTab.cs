using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableInteractionsTab : MonoBehaviour
{
    public Texture2D _cursorIcon;
    private Inventory _inventory;
    private ItemInteraction _playerItemInteraction;
    private Color normalButtonColorHexadecimal = Color.red;
    void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        _playerItemInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemInteraction>(); 
    }
    public void OpenInteractionTab()
    {
        Debug.Log("HELLO");
        RestartItemInteractionProperties();
        CheckAndCloseIfAnyTabOpen();
        ResetCursor();

        //interaction tabını aç
        var parentSlot = transform.parent;
        var interactionTab = parentSlot.transform.GetChild(0);
        interactionTab.gameObject.SetActive(true);

        //Butonların etkileşilebirliğini ve görünüşünü ayarla
        var itemReference = parentSlot.GetComponent<ItemReference>().currentItemReference;
        AdjustInteractionTabButtons(interactionTab, itemReference);
    }

    private void AdjustInteractionTabButtons(Transform interactionTab, ItemInterface itemReference)
    {
        if (!itemReference.Usable)
        {
            var button = interactionTab.transform.GetChild(0).gameObject.GetComponent<Button>();
            button.interactable = false;
            button.image.color = Color.gray;
        }
        else
        {
            var button = interactionTab.transform.GetChild(0).gameObject.GetComponent<Button>();
            button.interactable = true;
            button.image.color = normalButtonColorHexadecimal;
        }

        if (!itemReference.Interactable)
        {
            var button = interactionTab.transform.GetChild(2).gameObject.GetComponent<Button>();
            button.interactable = false;
            button.image.color = Color.gray;
        }
        else
        {
            var button = interactionTab.transform.GetChild(2).gameObject.GetComponent<Button>();
            button.interactable = true;
            button.image.color = normalButtonColorHexadecimal;
        }
    }

    private static void ResetCursor() => Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    private void RestartItemInteractionProperties()
    {
        if (_playerItemInteraction.interactionActive)
        {
            _playerItemInteraction.interactionActive = false;
        }
        if (_playerItemInteraction.ActiveItemID != 0)
        {
            _playerItemInteraction.ActiveItemID = 0;
        }
        if (_playerItemInteraction.CurrentItemSlot is not null)
        {
            _playerItemInteraction.CurrentItemSlot = null;
        }
    }
    private void CheckAndCloseIfAnyTabOpen()
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
