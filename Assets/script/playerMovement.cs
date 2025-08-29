using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private float xAxis;
    [SerializeField] private float walkspeed = 6f;
    
    [SerializeField] private float jumpForce = 2f;
    private bool isgrounded;
    private bool isPiked;
    public int jumMax = 2;
    private int currentJump = 0;

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


        if (Input.GetButtonDown("Jump") && (isgrounded || currentJump < jumMax))
        {

            rb.velocity = new Vector2(rb.velocity.x, 10f);
            currentJump += 1;
            Debug.Log(currentJump);
        }
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        
        
    }
    private void Move()
    {
        
        rb.velocity = new Vector2(walkspeed * xAxis, rb.velocity.y);

    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("floor"))
        {
            
            isgrounded = true;
            currentJump = 0;
            isPiked = false;
        }
        if (collision.gameObject.tag.Equals("spike"))
            
        {
            int i = 1;
            bool endingGame = false;
            // toujours mettre une deuxieme condition /:
            // -> evite de faire crash a cause de la boucle
            // et de perdre 1h de travail car pas save
            while (true && i < 5 && endingGame == false)
            {


                GameObject lifes = GameObject.Find("lifes" + " " + "(" + i + ")");
                
                if (lifes == null)
                {
                    if (i >= 3)
                    {

                        endingGame = true;
                        break;
                    }
                    else
                    {
                        i += 1;
                    }


                }
                else
                {
                    if (i == 3)
                    {
                        endingGame = true;
                    }
                    Destroy(lifes);
                    
                    break;
                }



            }




            if (endingGame)
            {
            
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
            isPiked = true;
            rb.velocity = new Vector2(walkspeed * -1, rb.velocity.y + 6);
            
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("floor")){
            
            isgrounded = false;
        }
       
    }
}
