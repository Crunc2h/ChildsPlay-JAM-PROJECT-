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
    private ItemInteraction _playerItemInteraction;
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
        SetItemInteractionProperties(itemIconObject);
    }

    

    void Start()
    {
        Usable = _isUsable;
        Interactable = _isInteractable;
        _playerItemInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemInteraction>();
    }

    private void ChangeCursor(GameObject itemIconObject) => Cursor.SetCursor(text, Vector2.zero, CursorMode.ForceSoftware);
    private void SetItemInteractionProperties(GameObject itemIconObject)
    {
        _playerItemInteraction.interactionActive = true;
        _playerItemInteraction.ActiveItemID = ItemId;
        _playerItemInteraction.CurrentItemSlot = itemIconObject.transform.parent.gameObject;
    }
}
