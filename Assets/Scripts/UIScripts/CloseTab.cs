using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTab : MonoBehaviour
{
    public void CloseTabFunction() => transform.parent.gameObject.SetActive(false);
}
