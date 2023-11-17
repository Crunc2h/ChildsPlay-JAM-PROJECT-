using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = default;
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        transform.position = new Vector3(transform.position.x + horizontalInput * moveSpeed * Time.deltaTime, 
            transform.position.y + verticalInput * moveSpeed * Time.deltaTime, 
            transform.position.z);
    }

}
