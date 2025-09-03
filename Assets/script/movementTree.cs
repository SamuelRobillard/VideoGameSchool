using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementTree : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this in the Inspector
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() // 
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // 

        // Set the horizontal velocity
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }
}
