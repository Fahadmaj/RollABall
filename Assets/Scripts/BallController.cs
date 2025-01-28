using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f; // Speed multiplier for rolling
    public float jumpForce = 5f; // Force applied for jumping
    private Rigidbody rb;
    private bool isGrounded = false; // Updated to false by default

    void Start()
    {
        // Get the Rigidbody component attached to the ball
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input from keyboard
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Apply force to the Rigidbody for rolling
        rb.AddForce(movement * speed);
    }

    void Update()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Apply upward force for jumping
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Disable jumping until we confirm landing
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the ball is touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Re-enable jumping when grounded
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Detect when the ball is no longer touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Disable jumping when not grounded
        }
    }
}