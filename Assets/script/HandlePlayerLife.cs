using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
public class HandlePlayerLife : MonoBehaviour
{

    [SerializeField] PlayerMovement playerMovement;
    private int numberOfBeingDead = 0;
    private int viesRestante = 3;
    private bool lostAllLife = false;
    private bool gameIsEnd = false;

    private bool hasPasscheckpoint1 = false;
    private bool hasPasscheckpoint2 = false;
    [SerializeField] GameObject checkpointDefault;
    [SerializeField] GameObject checkpoint1;
    [SerializeField] GameObject checkpoint2;
    [SerializeField] GameObject lifes1;
    [SerializeField] GameObject lifes2;
    [SerializeField] GameObject lifes3;
    [SerializeField] ResetTheGame resetTheGame;

    // Start is called before the first frame update
    void Awake()
    {
     
       
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasPasscheckpoint1)
        {
            
            if (playerMovement.getXPosition() > checkpoint1.transform.position.x)
            {
               
                hasPasscheckpoint1 = true;
            }
        }
         else if(!hasPasscheckpoint2)
        {
          if (playerMovement.getXPosition() > checkpoint2.transform.position.x)
            {
                hasPasscheckpoint2 = true;
            }  
        }
        
    }

    public void winALife()
    {
        if (viesRestante < 3 && !lostAllLife)
        {
            viesRestante += 1;
            if (viesRestante == 3) {
                     lifes1.GetComponent<SpriteRenderer>().enabled = true;
                }
                else if (viesRestante == 2) {
                     lifes2.GetComponent<SpriteRenderer>().enabled = true;
                }
                else if  (viesRestante == 1) {
                     lifes3.GetComponent<SpriteRenderer>().enabled = true;
                }
            
        }


    }

    public async void loseALifeVersionRenderer()
    {
        if (!lostAllLife && !gameIsEnd)
        {
        playerMovement.animator.SetBool("isHit", true);
            try
            {
                if (viesRestante == 3) {
                     lifes1.GetComponent<SpriteRenderer>().enabled = false;
                }
                else if (viesRestante == 2) {
                     lifes2.GetComponent<SpriteRenderer>().enabled = false;
                }
                else if  (viesRestante == 1) {
                     lifes3.GetComponent<SpriteRenderer>().enabled = false;
                }
            
                
                viesRestante -= 1;
                Color color;
                if (ColorUtility.TryParseHtmlString("#EC8585", out color))
                {
                    playerMovement.spriteRenderer.color = color;
                    await Task.Delay(1000);
                    playerMovement.spriteRenderer.color = Color.white;
                }

            }
            catch
            {
                
                if (!gameIsEnd)
                {
                gameIsEnd = true;
                await Task.Delay(2000);
                endingGame();
                }
                
                return;
        }
        if (viesRestante == 0)
            {
                lostAllLife = true;
                playerMovement.animator.SetTrigger("isDead");
                Rigidbody2D rb = playerMovement.GetComponent<Rigidbody2D>();
                rb.GetComponent<PlayerMovement>().enabled = false;
                await Task.Delay(2000);
                if (!gameIsEnd)
                {
                    await Task.Delay(2000);
                    gameIsEnd = true;
                    endingGame();
                }
            }
        }
     
    }
    private void endingGame()
    {
        Debug.Log(numberOfBeingDead);
        if (numberOfBeingDead > 0)
        {
            SceneManager.LoadScene("defeat");
        }
        else
        {
            numberOfBeingDead = 1;
        }
        

        if (hasPasscheckpoint2)
        {
            playerMovement.transform.position = new Vector2(checkpoint2.transform.position.x, checkpoint2.transform.position.y);
        }
        else if (hasPasscheckpoint1)
        {
            playerMovement.transform.position = new Vector2(checkpoint1.transform.position.x, checkpoint1.transform.position.y);
        }
        else
        {
            playerMovement.transform.position = new Vector2(checkpointDefault.transform.position.x, checkpointDefault.transform.position.y);
        }
        // reset les variables lorsque le joueur meurt
        viesRestante = 3;
        lostAllLife = false;
        gameIsEnd = false;
        resetTheGame.Reset();
        Rigidbody2D rb = playerMovement.GetComponent<Rigidbody2D>();
        rb.GetComponent<PlayerMovement>().enabled = true;
    }
}
