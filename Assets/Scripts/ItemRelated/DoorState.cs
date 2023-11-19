using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DoorState : MonoBehaviour
{
    public bool _doorLocked = false;
    [SerializeField] private GameObject _point;
    [SerializeField] private AudioSource _doorLockedSFX = null;
    [SerializeField] private AudioSource _doorOpenSFX = null;
    private GameObject _player;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Teleport()
    {
        _doorOpenSFX.Play();
        _player.transform.position = new Vector3(_point.transform.localToWorldMatrix.GetPosition().x,
        _point.transform.localToWorldMatrix.GetPosition().y, 0);
    }
    public void FailToOpenDoor()
    {
        _doorLockedSFX.Play();
    }
    
}
