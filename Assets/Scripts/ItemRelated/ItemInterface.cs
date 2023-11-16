using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemInterface
{
    bool Usable { get; set; }
    bool Interactable { get; set; } 
    void Use();
    void Interact();
}
