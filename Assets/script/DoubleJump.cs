using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class DoubleJump : MonoBehaviour
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
        // permet au joueur de sauter 2 foix
        // et detruit l'objet que le joueuer a ramasser

        playerMovement.setJumMax(2);
        playerMovement.setIsGrounded(true);
        Object.Destroy(GameObject.Find("doubleJump"));

    }
    
}
