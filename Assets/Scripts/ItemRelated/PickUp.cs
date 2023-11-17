using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory _inventory = null;
    
    public GameObject itemIcon;
    public GameObject itemPrefab;

    void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void Update()
    {
        CheckMouseClick();
    }

    private void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.tag == "Item")
            {
                CollectItem(hit.collider.gameObject);
            }
        }
    }
    private void CollectItem(GameObject item)
    {
        for (int i = 0; i < _inventory.slots.Length; i++)
        {
            if (_inventory.isFull[i] == false)
            {
                _inventory.isFull[i] = true;
                
                _inventory.slots[i].GetComponent<ItemReference>().currentItemReference = gameObject.GetComponent<Item>();
                
                //itemi yok etmeden önce item prefabını instantiatleyip deaktive et droplanırsa sonradan spawn edebilmek için
                _inventory.slots[i].GetComponent<ItemReference>().itemPrefab = GameObject.Instantiate(itemPrefab);
                _inventory.slots[i].GetComponent<ItemReference>().itemPrefab.SetActive(false);
                
                //item iconunu spawnla
                Instantiate(itemIcon, _inventory.slots[i].transform, false);
                
                Destroy(item);
                break;
            }
        }
    }
}
