using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 _playerPos = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = new Vector3(_playerPos.x, _playerPos.y, transform.position.z);
    }
}
