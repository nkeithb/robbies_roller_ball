using UnityEngine;
using System.Collections;

public class AnitPlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Random.Range (-50,50);
        float moveVertical = Random.Range(-50,50);
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("DontPickUp"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
