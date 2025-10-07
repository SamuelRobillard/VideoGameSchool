using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Threading.Tasks;
using NUnit.Framework;
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


    public bool hasFallentYet = false;

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
        // verifie si le joueur est dans une maison
        if (isInside)
        {
            quitInsideHouse(5f, 12f, 4f, -1.8f);
        }
        // vérifie si le joueur est tombé en dehors des limites
        hasFallTooLow(LowestGround);

        if (!isPiked)
        {
        // si le joueur n'est pas en train de subir un degat de pique, il peut bouger
            GetInputs();
            Move();
        }




        //logique pour la gestion du saut
        // vérifie que le joeur est sur un sol et que son nombre max de saut n'est pas dépacer
        if ((Input.GetButtonDown("Jump") && isgrounded) || (Input.GetButtonDown("Jump") && currentJump < jumMax))
        {
            
            if (currentJump < 2)
            {   
                audioSource.PlayOneShot(sfxJump);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 10f);

                currentJump += 1;
            }


        }
    }


   
    private void hasFallTooLow(int minimumY)
    {
        // si le joueur tombe trop bas. lance la scenbe de defaite et stock la scene actuelle
        if (rb.position.y < minimumY)
        {
            handleScene.lastScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("defeat");

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
        //voie de quel cote le player bouge et tourne le sprite
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
        // fait bouger le joueur dans le sens qu'il va
        rb.linearVelocity = new Vector2(walkspeed * xAxis, rb.linearVelocity.y);
        if (xAxis != 0 && isgrounded && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(sfxWalk);
        }
        yaxis = Input.GetAxisRaw("Vertical");
        // si le joueur ne marche plus et qu'il n'est pas en train de sauter, on désactive le son 
        if (xAxis == 0 && !(rb.linearVelocity.y > 0))
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
    // si le joueur touche le seul il peut re sauter
    {
        // permet de réinitialiser le saut lorsque le joueur est sur un sol
        if (collision.gameObject.tag.Equals("floor"))
        {

            isgrounded = true;
            currentJump = 0;
            isPiked = false;
        }
        
        if (collision.gameObject.tag.Equals("spike"))


        {
            // lors de la colision avec un pique, le joueur perd une vie et il est projeter vers l'arriere(la gauche)
            handlePlayerLife.loseALifeVersionRenderer();
            isPiked = true;
            rb.linearVelocity = new Vector2(walkspeed * -1, rb.linearVelocity.y + 6);

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
    
    
}
