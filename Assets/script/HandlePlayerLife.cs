using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
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

        // regarde si le joueur dépace un checkpoint
        if (!hasPasscheckpoint1)
        {

            if (playerMovement.getXPosition() > checkpoint1.transform.position.x)
            {

                hasPasscheckpoint1 = true;
            }
        }
        else if (!hasPasscheckpoint2)
        {
            if (playerMovement.getXPosition() > checkpoint2.transform.position.x)
            {
                hasPasscheckpoint2 = true;
            }
        }

    }

    public void winALife()
    {
        // redonne une vie au joueur en activant l'image de la vie correspondente
        if (viesRestante < 3 && !lostAllLife)
        {
            viesRestante += 1;
            if (viesRestante == 3)
            {
                lifes1.GetComponent<UnityEngine.UI.Image>().enabled = true;
            }
            else if (viesRestante == 2)
            {
                lifes2.GetComponent<UnityEngine.UI.Image>().enabled = true;
            }
            else if (viesRestante == 1)
            {
                lifes3.GetComponent<UnityEngine.UI.Image>().enabled = true;
            }

        }


    }

    public async void loseALifeVersionRenderer()
    {
        // enleve une vie au joueur en désactivant l'image de la vie correspondente
        if (!lostAllLife && !gameIsEnd)
        {

            playerMovement.animator.SetBool("isHit", true);
            try

            {
                // selectionne la bonne image
                if (viesRestante == 3)
                {
                    lifes1.GetComponent<UnityEngine.UI.Image>().enabled = false;
                }
                else if (viesRestante == 2)
                {
                    lifes2.GetComponent<UnityEngine.UI.Image>().enabled = false;
                }
                else if (viesRestante == 1)
                {
                    lifes3.GetComponent<UnityEngine.UI.Image>().enabled = false;
                }


                viesRestante -= 1;
                Color color;
                // change la couleur du sprite
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
                // si le joueur perd toute ces vies, on déclache une animation de mort
                // puit on attend un peu et on lance la methode qui va arreter la partie.
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
    public void endingGame()
    {
        
        Debug.Log(numberOfBeingDead);
        // si le joueur est deja mort plus d'une foix, la scene de défaite se lance
        if (numberOfBeingDead > 0)
        {
            handleScene.lastScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("defeat");
        }
        else
        {
            numberOfBeingDead = 1;
        }

        // fait réapparaitre le joueur au bon endroit.
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
