using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]


public class SimpleEnemyPatrol : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    
    public Transform leftPoint, rightPoint;

    public float speed = 2f;
    public int touchDamage = 1;
    private bool toRight = true;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    private float lastHitTime = -9999f;
    private float cooldown = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //  animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // si le joueur se trouve dans une certaine zone
        if (playerMovement.getXPosition() > 20 && playerMovement.getXPosition() < 29)
        {
            // si le temps d'attente est réspecter, l'ennemi fait une animation d'attaque
            if (Time.time > lastHitTime + cooldown)
            {
                Debug.Log("trigger");
                animator.SetTrigger("isAttacking");
                lastHitTime = Time.time;
            }
            // dis dans quelle direction le joueur est par rapport a l'ennemi
            // et tourne l'ennemi en conséquant
            if (playerMovement.getXPosition() < transform.position.x)
            {
                toRight = false;


            }
            else
            {
                toRight = true;



            }
        }
        else
        {
            // tourne le sprtie de l'enemie dépendement dans quelle direction il se dirige.
            if (toRight && transform.position.x >= rightPoint.position.x) toRight = false;
            else if (!toRight && transform.position.x <= leftPoint.position.x) toRight = true;



        }
        // fait avancer l'ennmie dans la direction qu'il regarde
        float dir = toRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
        sr.flipX = !toRight;
        }
       

    // void OnCollisionEnter2D(Collision2D col)
    // {
    // if (col.collider.CompareTag("Player"))
    // {
    // var hp = col.collider.GetComponent<PlayerHealth>();
    // if (hp) hp.TakeDamage(touchDamage);
    // }
    // }
}
