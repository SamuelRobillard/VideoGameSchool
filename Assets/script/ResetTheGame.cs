using UnityEngine;

public class ResetTheGame : MonoBehaviour

{
    [SerializeField] PlayerHitEnemy playerHitEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Reset()
    {
        GameObject lifes1 = GameObject.Find("lifes (1)");
        GameObject lifes2 = GameObject.Find("lifes (2)");
        GameObject lifes3 = GameObject.Find("lifes (3)");

        lifes1.GetComponent<SpriteRenderer>().enabled = true;
        lifes2.GetComponent<SpriteRenderer>().enabled = true;
        lifes3.GetComponent<SpriteRenderer>().enabled = true;
        
        GameObject potion = GameObject.Find("potion");
        potion.GetComponent<SpriteRenderer>().enabled = true;
        potion.GetComponent<BoxCollider2D>().enabled = true;

        GameObject enemy = GameObject.Find("enemy");
        enemy.GetComponent<SpriteRenderer>().enabled = true;
        enemy.GetComponent<BoxCollider2D>().enabled = true;
        GameObject enemyChild = GameObject.Find("colliderCircle");
        enemyChild.GetComponent<BoxCollider2D>().enabled = true;
        playerHitEnemy.setNumberOfBeingHit(0);

    }
}
