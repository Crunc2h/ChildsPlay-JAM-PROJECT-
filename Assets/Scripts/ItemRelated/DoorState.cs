using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DoorState : MonoBehaviour
{
    public bool _doorLocked = false;
    [SerializeField] private GameObject _point;
    private GameObject _player;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Teleport()
    {
        _player.transform.position = new Vector3(_point.transform.localToWorldMatrix.GetPosition().x,
        _point.transform.localToWorldMatrix.GetPosition().y, 0);
    }
    public void FailToOpenDoor()
    {

    }
    
}
