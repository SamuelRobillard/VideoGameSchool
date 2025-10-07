using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerMovement playerMovement;
    private HandlePlayerLife handlePlayerLife;
    void Start()
    {
        handlePlayerLife = playerMovement.GetComponent<HandlePlayerLife>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            // si l'objet qui rentre en contact est un Player, lui redonne une vie et désactive le visuel de la potion
            handlePlayerLife.winALife();
            GameObject potion = GameObject.Find("potion");
            potion.GetComponent<SpriteRenderer>().enabled = false;
            potion.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
