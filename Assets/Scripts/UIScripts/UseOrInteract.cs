using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UseOrInteract : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUserClick()
    {
        var itemReference = transform.GetComponentInParent<ItemReference>(false);
        if(gameObject.tag == "UseButton")
        {
            itemReference.currentItemReference.Use();
        }
        else if(gameObject.tag == "InteractButton")
        {
            itemReference.currentItemReference.Interact();
        }
    }
}
