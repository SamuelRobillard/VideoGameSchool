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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //  animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (playerMovement.getXPosition() > 20 && playerMovement.getXPosition() < 29)
        {
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

            if (toRight && transform.position.x >= rightPoint.position.x) toRight = false;
            else if (!toRight && transform.position.x <= leftPoint.position.x) toRight = true;



        }
        float dir = toRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
        sr.flipX = toRight;
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
