using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float _smoothness = 1f;
    private Vector3 _playerPos = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 targetPosition = new Vector3(_playerPos.x, _playerPos.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothness * Time.deltaTime);
    }
}
