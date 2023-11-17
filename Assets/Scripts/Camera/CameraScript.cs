using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float _smoothness = default;
    private GameObject _player = default;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        var playerPosition = _player.transform.position;
        Vector3 targetPosition = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothness * Time.deltaTime);
    }
}
