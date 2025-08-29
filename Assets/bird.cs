using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class bird : MonoBehaviour
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
        GameObject player = GameObject.Find("ScareCrow");
        Rigidbody2D rbPlayer = player.GetComponent<Rigidbody2D>();

        playerMovement.jumMax = 2;
    }
}
