using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReceiveInteraction : MonoBehaviour
{
    public int[] RelevantItemIDs;
    private GameObject _player;
    private GameObject _currentItemSlot;
    [SerializeField] private float _minimumInteractionDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckInteractionDistance())
        {
            CheckMouseClick();
        }
    }
    private void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.tag == "InteractableObject")
            {
                if(_player.GetComponent<ItemInteraction>().interactionActive &&
                    RelevantItemIDs.Contains(_player.GetComponent<ItemInteraction>().ActiveItemID))
                {
                    SuccessfulInteraction(_player.GetComponent<ItemInteraction>().CurrentItemSlot);
                }
                else
                {
                    FailedInteraction();
                }
            }
        }
    }
    private bool CheckInteractionDistance() => Vector3.Distance(transform.position,
        _player.transform.position) < _minimumInteractionDistance;

    private void SuccessfulInteraction(GameObject currentItemSlot)
    {
        Debug.Log("Success!!");
        //Destroy icon
        
        
        //Empty slot
        for(int i = 0; i < _player.GetComponent<Inventory>().slots.Count(); i++)
        {
            if(currentItemSlot == _player.GetComponent<Inventory>().slots[i])
            {
                _player.GetComponent<Inventory>().isFull[i] = false;
                break;
            }
        }
        var itemIcon = currentItemSlot.transform.GetChild(1).gameObject;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);

        Destroy(itemIcon);
    }
    private void FailedInteraction()
    {
        Debug.Log("Failure!!");
    }
}
