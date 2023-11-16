using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from WASD keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the player
        transform.position = new Vector3(transform.position.x + horizontalInput * moveSpeed * Time.deltaTime, 
            transform.position.y + verticalInput * moveSpeed * Time.deltaTime, 
            transform.position.z);
    }

}
