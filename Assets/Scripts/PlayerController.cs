using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    // Sets Rigidbody name to rb in script
    private Rigidbody rb;
    // Sets rb to get the component Rigidbody
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Checks for input from keyboard to determine Horizontal and Vertical movement of "Player"
    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
    //Calls to rb to add force to the player object according to the (X,Y,Z) Vector Coordinates
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);
    }
}
