using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] AudioClip sfxJump;
    [SerializeField] AudioClip sfxWalk;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float xAxis;
    private float yaxis;
    [SerializeField] private float walkspeed = 6f;

    [SerializeField] private float jumpForce = 10f;
    private bool isgrounded;
    private bool isPiked;
    [SerializeField] private int jumMax = 1;
    public bool isInside = false;

    private int currentJump = 0;
    private AudioSource audioSource;
    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (isInside)
        {
            quitInsideHouse(5f, 12f, 4f, -1.8f);
        }

        hasFallTooLow();
        if (!isPiked)
        {
            GetInputs();
            Move();
        }
        if (Input.GetKey(KeyCode.X))
        {
            animator.SetBool("Attack", true); // lance l’animation
           
        }
        else
        {
            animator.SetBool("Attack", false); // arrête l’animation
        }

        if ((Input.GetButtonDown("Jump") && isgrounded) || (Input.GetButtonDown("Jump") && currentJump < jumMax))
        {

            if (currentJump < 2)
            {
                audioSource.PlayOneShot(sfxJump);
                rb.velocity = new Vector2(rb.velocity.x, 10f);
                Debug.Log(currentJump + " " + isgrounded);
                currentJump += 1; 
            }
            

        }
    }

    private void hasFallTooLow()
    {
        if (rb.position.y < -10)
        {
            transform.position = new Vector2(4f, -1.8f);
            loseALife();
        }
    }
    public void quitInsideHouse(float xOrigine, float yOrigine, float xDest, float yDest)
    {
        if (rb.position.x < xOrigine && rb.position.y > yOrigine)
        {
            transform.position = new Vector2(xDest, yDest);
        }

    }
    public float getXPosition()
    {
        Debug.Log("this is pos " + rb.position.x);
        return rb.position.x;
    }
    public float getYposition()
    {
        return rb.position.y;
    }
    public void setXYposition(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        if(xAxis != 0){
            animator.SetFloat("X", 1);
        }
        else{
            animator.SetFloat("X", 0);
        }
        
        if (xAxis > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (xAxis < 0)
        {
            spriteRenderer.flipX = true;
        }
       
       
    }
    private void Move()
    {

        rb.velocity = new Vector2(walkspeed * xAxis, rb.velocity.y);
        if (xAxis != 0 && isgrounded && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(sfxWalk);
        }
        yaxis = Input.GetAxisRaw("Vertical");
        Debug.Log(yaxis);
        if (xAxis == 0 && yaxis! > 0)
        {
            audioSource.Stop();
        }
    }

    public void setJumMax(int value)
    {
        jumMax = value;
    }
    public void setIsGrounded(bool value)
    {
        isgrounded = value;
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
            animator.SetBool("isHit", true);
            loseALife();
            isPiked = true;
            rb.velocity = new Vector2(walkspeed * -1, rb.velocity.y + 6);
            
        }
    }

    private void loseALife()
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
    }
    
    void OnCollisionExit2D(Collision2D collision)

    {
        animator.SetBool("isHit", false);
        if(collision.gameObject.tag.Equals("floor")){
            isgrounded = false;
            
        }
       
    }
}
