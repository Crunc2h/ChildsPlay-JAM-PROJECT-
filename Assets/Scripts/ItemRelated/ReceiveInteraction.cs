using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReceiveInteraction : MonoBehaviour
{
    public int[] RelevantItemIDs;
    private GameObject _player;
    private ItemInteraction _playerItemInteraction;
    private GameObject _currentItemSlot;
    [SerializeField] private float _minimumInteractionDistance;
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
        _playerItemInteraction = _player.GetComponent<ItemInteraction>();
    }

    void Update()
    {
        if(CheckInteractionDistance())
        {
            CheckMouseClick();
        }
    }
    private void CheckMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.tag == "InteractableObject")
            {
                if(_playerItemInteraction.interactionActive &&
                    RelevantItemIDs.Contains(_playerItemInteraction.ActiveItemID))
                {
                    SuccessfulInteraction(_playerItemInteraction.CurrentItemSlot);
                }
                else
                {
                    FailedInteraction();
                }
            }

            if(hit.collider != null && hit.collider.tag == "Door")
            {
                if(!hit.collider.gameObject.GetComponent<DoorState>()._doorLocked)
                {
                    hit.collider.gameObject.GetComponent<DoorState>().Teleport();
                }
                else
                {
                    hit.collider.gameObject.GetComponent<DoorState>().FailToOpenDoor();
                }
            }

            if(hit.collider != null && hit.collider.tag == "Robot")
            {
                GameObject.FindGameObjectWithTag("Robot").GetComponent<QuestManager>().PlayerClick();
            }

            if (hit.collider != null && hit.collider.tag == "Cat")
            {
                var allItems = GameObject.FindGameObjectsWithTag("Item");
                var kediTüyleri = allItems.Where(_item => _item.GetComponent<Item>().GetItemId() == 6);
                kediTüyleri.First().transform.position = transform.position;
                //kedi ölme sfx
                Destroy(gameObject);

            }
        }
    }
    private bool CheckInteractionDistance() => Vector3.Distance(transform.position,
        _player.transform.position) < _minimumInteractionDistance;

    private void SuccessfulInteraction(GameObject currentItemSlot)
    {
        Debug.Log("Success!!");

        //Empty slot
        EmptyInventorySlot(currentItemSlot);
        ResetCursor();
        ResetItemInteractionProperties();

        //Destroy icon
        var itemIcon = currentItemSlot.transform.GetChild(1).gameObject;
        Destroy(itemIcon);
    }
    private static void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }
    private void ResetItemInteractionProperties()
    {
        _playerItemInteraction.interactionActive = false;
        _playerItemInteraction.ActiveItemID = default;
        _playerItemInteraction.CurrentItemSlot = null;
    }

    private void EmptyInventorySlot(GameObject currentItemSlot)
    {
        for (int i = 0; i < _player.GetComponent<Inventory>().slots.Count(); i++)
        {
            if (currentItemSlot == _player.GetComponent<Inventory>().slots[i])
            {
                _player.GetComponent<Inventory>().isFull[i] = false;
                break;
            }
        }
    }

    private void FailedInteraction()
    {
        Debug.Log("Failure!!");
    }
}
