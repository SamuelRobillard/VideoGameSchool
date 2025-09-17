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
    void Update()
    {
        isFacing();
        if (Input.GetKey(KeyCode.X))
        {
            
            isTouching();
           
        }
       
        
    }
    private void isTouching()
    {
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
            Debug.Log(playerMovement.getXPosition() -  rb.position.x);
            if (playerMovement.getXPosition() - rb.position.x < 0 && !(playerMovement.getXPosition() - rb.position.x < -2))
            {
                numberOfBeinghit += 1;
                Debug.Log(numberOfBeinghit);
            }
        }

        if (numberOfBeinghit == Pvnumber)
        {

            Debug.Log("adadsasdads");
            playerMovement.winALife();
            
            Destroy(gameObject);
        }
    }
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
