using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private GameObject _player;
    private GameObject _camera;
    [SerializeField] private GameObject _point1;
    [SerializeField] private GameObject _point2;
    [SerializeField] private float _teleportCooldown = 2f;
    private bool _teleportOnCooldown = false;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(Vector2.Distance(_player.transform.position, _point1.transform.localToWorldMatrix.GetPosition()) 
                < Vector2.Distance(_player.transform.position, _point2.transform.localToWorldMatrix.GetPosition()))
            {
                if(!_teleportOnCooldown)
                {
                    _player.transform.position = new Vector3(_point2.transform.localToWorldMatrix.GetPosition().x,
                    _point2.transform.localToWorldMatrix.GetPosition().y, 0);
                    _teleportOnCooldown = true;
                    StartCoroutine(Cooldown());
                }

            }
            else
            {
                if(!_teleportOnCooldown)
                {
                    _player.transform.position = new Vector3(_point1.transform.localToWorldMatrix.GetPosition().x,
                    _point1.transform.localToWorldMatrix.GetPosition().y, 0);
                    _teleportOnCooldown = true;
                    StartCoroutine(Cooldown());
                }
            }
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_teleportCooldown);
        _teleportOnCooldown = false;
    }
}
