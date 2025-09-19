using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHitEnemy : MonoBehaviour
{

    [SerializeField]PlayerMovement playerMovement;
    [SerializeField]Rigidbody2D rb;
    [SerializeField] int Pvnumber;
    private int numberOfBeinghit = 0;
    private bool playerIsAttackingTowardTheLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isFacing();
        if (Input.GetKey(KeyCode.X))
        {
            
            isTouching();
           
        }
    }
    void Update()
    {
        
       
        
    }
    private void isTouching()
    {

        //logique pour savoir si lors de l'attaque le joueur touche l'ennemie
        
        if (playerIsAttackingTowardTheLeft)

        {

            if (playerMovement.getXPosition() - rb.position.x < 2 && !(playerMovement.getXPosition() - rb.position.x < 0))
            {
                numberOfBeinghit += 1;
                Debug.Log(numberOfBeinghit);
            }
        }
        else
        {
            Debug.Log(playerMovement.getXPosition() - rb.position.x);
            if (playerMovement.getXPosition() - rb.position.x < 0 && !(playerMovement.getXPosition() - rb.position.x < -2))
            {
                numberOfBeinghit += 1;
                Debug.Log(numberOfBeinghit);
            }
        }

        if (numberOfBeinghit == Pvnumber)
        {
            // si l'ennemi est touchner plusieurs fois il meurt
            Debug.Log("adadsasdads");
            playerMovement.winALife();
            
            Destroy(gameObject);
        }
    }
    // regarde de quel cote le joueur regarde
    private void isFacing()
    {
        if (playerMovement.spriteRenderer.flipX == true)
        {
            playerIsAttackingTowardTheLeft = true;


        }
        else
        {
            playerIsAttackingTowardTheLeft = false;
        }
    }
}
