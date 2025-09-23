using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] AudioClip sfxJump;
    [SerializeField] AudioClip sfxWalk;
    [SerializeField] public AudioClip sfxAttack;
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float xAxis;
    private float yaxis;
    [SerializeField] private float walkspeed = 6f;
    [SerializeField] private int LowestGround;

    
    private bool isgrounded;
    private bool isPiked;
    [SerializeField] private int jumMax = 1;
    public bool isInside = false;

    private int currentJump = 0;
    public AudioSource audioSource;
    public Animator animator;

    private HandlePlayerLife handlePlayerLife;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        handlePlayerLife = GetComponent<HandlePlayerLife>();

    }

    // Update is called once per frame
    void Update()
    {

        if (isInside)
        {
            quitInsideHouse(5f, 12f, 4f, -1.8f);
        }

        hasFallTooLow(LowestGround);
        if (!isPiked)
        {
            GetInputs();
            Move();
        }
        



        //logique pour la gestion du saut

        if ((Input.GetButtonDown("Jump") && isgrounded) || (Input.GetButtonDown("Jump") && currentJump < jumMax))
        {

            if (currentJump < 2 && rb.velocity.y == 0)
            {
                audioSource.PlayOneShot(sfxJump);
                rb.velocity = new Vector2(rb.velocity.x, 10f);

                currentJump += 1;
            }


        }
    }


   
    private void hasFallTooLow(int minimumY)
    {
        if (rb.position.y < minimumY)
        {
            transform.position = new Vector2(4f, -1.8f);

            handlePlayerLife.loseALifeVersionRenderer();
        }
    }
    //teleporte le joueur a une position donne
    public void quitInsideHouse(float xOrigine, float yOrigine, float xDest, float yDest)
    {
        if (rb.position.x < xOrigine && rb.position.y > yOrigine)
        {
            transform.position = new Vector2(xDest, yDest);
        }

    }
    public float getXPosition()
    {
        
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
        //voie de quel cote le player bouge et tourne le spirte
        xAxis = Input.GetAxisRaw("Horizontal");
        if (xAxis != 0)
        {
            animator.SetFloat("X", 1);
        }
        else
        {
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
        
        if (xAxis == 0 && !(rb.velocity.y > 0))
        {
            Debug.Log(rb.velocity.y);
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
    // si le joueur touche le seul il peut re sauter
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
            handlePlayerLife.loseALifeVersionRenderer();
            isPiked = true;
            rb.velocity = new Vector2(walkspeed * -1, rb.velocity.y + 6);

        }
    }


    
    // desactive l'option de saut
    void OnCollisionExit2D(Collision2D collision)

    {
        animator.SetBool("isHit", false);
        if (collision.gameObject.tag.Equals("floor"))
        {
            isgrounded = false;

        }

    }
    
    // redone les vies au joeueur et restart la scene
    
}
