using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAYERFIX : MonoBehaviour
{
    [SerializeField] private int _targetLayer;
    private int _originalLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _originalLayer = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = _targetLayer;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = _originalLayer;
        }
    }
}
