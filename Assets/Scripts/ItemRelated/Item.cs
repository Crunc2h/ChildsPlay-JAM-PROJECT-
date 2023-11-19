using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
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
    [SerializeField] private AudioSource _openFiş = null;
    public Texture2D cursorTexture;
    public bool Usable { get; set; }
    public bool Interactable { get; set; }
    public void Interact(GameObject parentSlot)
    {
        Debug.Log("INTERACT");
        OpenFişTab(parentSlot);
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

    private void ChangeCursor(GameObject itemIconObject) => Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    private void SetItemInteractionProperties(GameObject itemIconObject)
    {
        _playerItemInteraction.interactionActive = true;
        _playerItemInteraction.ActiveItemID = ItemId;
        _playerItemInteraction.CurrentItemSlot = itemIconObject.transform.parent.gameObject;
    }

    private void OpenFişTab(GameObject parentSlot)
    {
        var fişTab = GameObject.FindGameObjectWithTag("canvas").transform.GetChild(0).gameObject;
        fişTab.SetActive(true);
        var text = fişTab.transform.GetChild(0).gameObject;
        var currentQuestText = GameObject.FindGameObjectWithTag("Robot").GetComponent<QuestManager>()._currentFişString;
        text.GetComponent<TextMeshProUGUI>().text = currentQuestText;
        var interactionTab = parentSlot.transform.GetChild(0).gameObject;
        if(interactionTab.activeSelf)
        {
            interactionTab.SetActive(false);
        }
    }

    public int GetItemId() => ItemId;
}
