using UnityEngine;
using System.Collections;

public class AnitPlayerController : MonoBehaviour {

    public float speed;
    
    private Rigidbody rb;
    private int Position;
    private Rigidbody Player;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

   void FixedUpdate()
    {
        float moveHorizontal = Random.Range (-10,10);
        float moveVertical = Random.Range(-10,10);
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }
    
   
}
