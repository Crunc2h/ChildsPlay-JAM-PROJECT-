using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ItemInterface
{
    [SerializeField] private bool _isUsable = false;
    [SerializeField] private bool _isInteractable = false;
    [SerializeField] private GameObject _itemPrefab;
    public bool Usable { get; set; }
    public bool Interactable { get; set; }



    public void Interact()
    {
        Debug.Log("INTERACT");
    }

    public void Use()
    {
        Debug.Log("USE");
    }

    // Start is called before the first frame update
    void Start()
    {
        Usable = _isUsable;
        Interactable = _isInteractable;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
