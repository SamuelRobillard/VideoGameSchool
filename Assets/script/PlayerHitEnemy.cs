using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerHitEnemy : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int Pvnumber;
    private HandlePlayerLife handlePlayerLife;

    private int numberOfBeinghit = 0;
    private bool playerIsAttackingTowardTheLeft;

    private IsAttacking isAttacking;
    private bool hasHitThisAttack = false; // ✅ nouveau flag

    void Start()
    {
        isAttacking = playerMovement.GetComponent<IsAttacking>();
        handlePlayerLife = playerMovement.GetComponent<HandlePlayerLife>();
    }

    void FixedUpdate()
    {
        isFacing();
    }

    void Update()
    {
        // appelle une methode qui vérifie si le joueur attaque en ce moment
        if (isAttacking != null && isAttacking.IsAttackingNow())
        {
            // verifie que l'ennemie n'a pas été toucher durant la meme attaque(évite de perde 2 vies en une seule attaque)
            if (!hasHitThisAttack)
            {
                if (isTouching())
                {
                    EnemyHit();
                    hasHitThisAttack = true; 
                }
            }
        }
        else
        {
            //  reset quand l’attaque est finie
            hasHitThisAttack = false;
        }
    }

    private bool isTouching()
    {
        // prend la distance entre le joueur et l'ennemie 
        float distance = playerMovement.getXPosition() - rb.position.x;
        // verifie si le joueur touche l'ennmie(depend de quelle dans direction le joueur frappe)
        if (playerIsAttackingTowardTheLeft)
        {
            return distance < 3 && distance > 0;
        }
        else
        {
            return distance < 0 && distance > -3;
        }
    }

    private void EnemyHit()
    {
        numberOfBeinghit += 1;


        // quand l'ennemie perd toutes ses vies, on rend son prite invisible et on désactive le boxCollider de son enfant.
        if (numberOfBeinghit >= Pvnumber)
        {
            
            handlePlayerLife.winALife();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject enemyChild = GameObject.Find("colliderCircle");

            enemyChild.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            StartCoroutine(FlashRed());
         
        }
    }
    private IEnumerator FlashRed()
{
    Color redColor;
    if (ColorUtility.TryParseHtmlString("#EC8585", out redColor))
    {
        gameObject.GetComponent<SpriteRenderer>().color = redColor;
        yield return new WaitForSeconds(0.5f); // wait 0.5 second
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
    private void isFacing()
    {
        // retounre dans quelle direction le joueur regarde
        playerIsAttackingTowardTheLeft = playerMovement.spriteRenderer.flipX;
    }
    public bool setNumberOfBeingHit(int number)
    {
        numberOfBeinghit = number;
        return true; 
    }
}
