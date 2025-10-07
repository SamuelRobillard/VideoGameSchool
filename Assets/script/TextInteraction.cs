using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.UI;

public class TextInteraction : MonoBehaviour
{
    
    public PlayerMovement playerMovement;
    private TextMeshProUGUI text;
    private bool allEnnemiesAreDead = false;
    // Start is called before the first frame update
    void Start()
    {

        GameObject textPro = GameObject.Find("TextInteract");
        text = textPro.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerMovement.getXPosition() > 3 && playerMovement.getXPosition() < 5 && playerMovement.getYposition() <5)
        {
            
            text.gameObject.SetActive(true);
            try
            {
                GameObject en = GameObject.Find("enemy");
                // si le sprite est false alors l'ennemie est vaicu

                allEnnemiesAreDead = !en.GetComponent<SpriteRenderer>().enabled;
                Debug.Log(allEnnemiesAreDead);
                Debug.Log(!en.GetComponent<SpriteRenderer>().enabled);
            }
            catch
            {
                allEnnemiesAreDead = true;
            }

            if (!allEnnemiesAreDead) {
                text.text = "Kill all Ennemies";
            }
            else
            {
                text.text = "Press Q";
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    playerMovement.setXYposition(10f, 19f);
                    playerMovement.isInside = true;
                }
            }
           
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }
}
