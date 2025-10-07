using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
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
        try
        {
            GameObject lifes1 = GameObject.Find("lifes (1)");
            GameObject lifes2 = GameObject.Find("lifes (2)");
            GameObject lifes3 = GameObject.Find("lifes (3)");
            Debug.Log("asdasd");
            lifes1.GetComponent<UnityEngine.UI.Image>().enabled = true;
            lifes2.GetComponent<UnityEngine.UI.Image>().enabled = true;
            lifes3.GetComponent<UnityEngine.UI.Image>().enabled = true;
        }
        catch
        {
            Debug.Log("not found");
        }


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
