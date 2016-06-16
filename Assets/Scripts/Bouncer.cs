using UnityEngine;
using System.Collections;

public class Bouncer : MonoBehaviour {

    public float force = 90.0f;
    public float height = 0.2f;

    private Rigidbody rb;
    private Vector3 startPos;
    private float startPosY;

    void Start ()
    {
        startPos = gameObject.transform.position;
        startPosY = startPos.y;
        rb = gameObject.GetComponent<Rigidbody>();
        BounceUp();
    }
	
	void FixedUpdate ()
    {
        BounceUp();
    }

    private void BounceUp ()
    {
        if (gameObject.transform.position.y <= startPosY + height)
        {
            rb.AddForce(new Vector3(0, force, 0));
            rb.ResetInertiaTensor();
        }       
    }
}
