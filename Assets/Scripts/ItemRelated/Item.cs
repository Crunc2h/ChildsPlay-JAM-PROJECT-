using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ItemInterface
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
