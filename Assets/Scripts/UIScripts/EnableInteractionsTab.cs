using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableInteractionsTab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenInteractionTab()
    {
        var parentSlot = transform.parent;
        var interactionTab = parentSlot.transform.GetChild(0);
        interactionTab.gameObject.SetActive(true);
    }
}
