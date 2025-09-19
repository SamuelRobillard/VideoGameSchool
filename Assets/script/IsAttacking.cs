using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class isAttacking : MonoBehaviour
{

    [SerializeField]PlayerMovement playerMovement;
    private int numberOfBeinghit = 0;
    private bool playerIsAttackingTowardTheLeft;
    public float cooldown = 0.5f;
    public float lastAttackedAt = -9999f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       

       
            
               
            
         
           
        
    }
    void Update()
    {
        
        isAttackingFunct();
        
    }

    public bool isAttackingFunct()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {

            Debug.Log(Time.time + " : " + lastAttackedAt);

            if (Time.time > lastAttackedAt + cooldown)
            {
                //do the attack
                lastAttackedAt = Time.time;

                playerMovement.animator.SetBool("Attack", true); // lance l’animation
                playerMovement.audioSource.PlayOneShot(playerMovement.sfxAttack);
                return true;
            }



        }
        else
        {
            playerMovement.animator.SetBool("Attack", false); // arrête l’animation
            return false;
        }
        return false;
    }
   
    // regarde de quel cote le joueur regarde
   
}
