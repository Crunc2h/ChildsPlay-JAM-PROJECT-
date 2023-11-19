using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float _smoothness = default;
    private GameObject _player = default;
    public bool _settingCameraPos = false;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        var playerPosition = _player.transform.position;
        Vector3 targetPosition = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
        if(!_settingCameraPos)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothness * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
        }
    }
    private void FixedUpdate()
    {
        
    }
}
