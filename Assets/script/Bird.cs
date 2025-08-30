using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Bird : MonoBehaviour
{


    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        playerMovement.setJumMax(2);
        playerMovement.setIsGrounded(true);

    }
    
}
