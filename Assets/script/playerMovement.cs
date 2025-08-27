using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private float xAxis;
    [SerializeField] private float walkspeed = 6f;
    
    [SerializeField] private float jumpForce = 2f;
    private bool isgrounded;
    private bool isPiked;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPiked)
        {
            GetInputs();
            Move();
        }
       
        
        if (Input.GetButtonDown("Jump") && isgrounded)
        {
            
            rb.velocity = new Vector2(rb.velocity.x, 10f);

        }
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        
        
    }
    private void Move()
    {
        Debug.Log(xAxis);
        rb.velocity = new Vector2(walkspeed * xAxis, rb.velocity.y);

    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("floor"))
        {
            Debug.Log("floor");
            isgrounded = true;
            isPiked = false;
        }
        if (collision.gameObject.tag.Equals("spike"))
        {



            isPiked = true;
            rb.velocity = new Vector2(walkspeed * -1, rb.velocity.y + 6);
            
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("floor")){
            Debug.Log("not floor");
            isgrounded = false;
        }
       
    }
}
