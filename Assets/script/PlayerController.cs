using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    private Vector3 movement = Vector3.zero;
    private Rigidbody rb;  // Declare a Rigidbody reference

    void Start()
    {
        // Get the Rigidbody component attached to the capsule
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Determine movement direction based on arrow key input
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            movement = Vector3.back;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            movement = Vector3.right;
        }
        else
        {
            movement = Vector3.zero;  // No movement if no key is pressed
        }
    }

    void FixedUpdate()
    {
        // Apply the movement to the Rigidbody using velocity
        if (movement != Vector3.zero)
        {
            rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
        }
    }
      void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the cube
        if (collision.gameObject.CompareTag("cube"))
        {
            Debug.Log("Capsule collided with the cube!");
        }
    }
}
