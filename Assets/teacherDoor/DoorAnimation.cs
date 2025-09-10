using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    public GameObject door;      // Assigner la porte dans l’Inspector
    private Animator doorAnimator;
 
    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
    }
 
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ads");
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("openDoor");
        }
    }
 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("closeDoor");
        }
    }
}