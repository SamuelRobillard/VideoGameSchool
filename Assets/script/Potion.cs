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
            handlePlayerLife.winALife();
            Object.Destroy(GameObject.Find("potion"));
        }
    }
}
