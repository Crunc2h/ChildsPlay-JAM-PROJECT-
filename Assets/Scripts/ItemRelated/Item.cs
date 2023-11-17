using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, ItemInterface
{
    [SerializeField] private int ItemId = default;
    [SerializeField] private bool _isUsable = false;
    [SerializeField] private bool _isInteractable = false;
    [SerializeField] private GameObject _itemPrefab;
    public Texture2D text;
    public bool Usable { get; set; }
    public bool Interactable { get; set; }
  
    public void Interact()
    {
        Debug.Log("INTERACT");
    }
    public void Use(GameObject itemIconObject)
    {
        Debug.Log("USE");
        ChangeCursor(itemIconObject);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ItemInteraction>().interactionActive = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<ItemInteraction>().ActiveItemID = ItemId;
        GameObject.FindGameObjectWithTag("Player").GetComponent<ItemInteraction>().CurrentItemSlot = itemIconObject.transform.parent.gameObject;
    }
    void Start()
    {
        Usable = _isUsable;
        Interactable = _isInteractable;
    }

    private void ChangeCursor(GameObject itemIconObject)
    {
        Cursor.visible = false;
        Cursor.SetCursor(text, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.visible = true;
    }

}
