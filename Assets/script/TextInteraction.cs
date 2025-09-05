using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextInteraction : MonoBehaviour
{
    
    public PlayerMovement playerMovement;
    private GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        
        text = GameObject.Find("TextInteract");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerMovement.getXPosition());
        if (playerMovement.getXPosition() > 3 && playerMovement.getXPosition() < 5 && playerMovement.getYposition() <5)
        {
            text.SetActive(true);
            if (Input.GetKey(KeyCode.Q))
            {
                playerMovement.setXYposition(10f, 19f);
                playerMovement.isInside = true;
            }
        }
        else
        {
            text.SetActive(false);
        }
    }
}
