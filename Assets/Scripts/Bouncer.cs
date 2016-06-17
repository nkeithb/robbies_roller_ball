using UnityEngine;
using System.Collections;

public class Bouncer : MonoBehaviour {

    [Tooltip("Bounce force")]
    public float force = 90.0f;
    [Tooltip("Minimum distance from starting position to trigger a bounce")]
    public float threshold = 0.2f;

    private Rigidbody rb;
    private Vector3 startPos;
    private float startPosY;

    void Start ()
    {
        startPosY = gameObject.transform.position.y;
        rb = gameObject.GetComponent<Rigidbody>();
    }
	
	void FixedUpdate ()
    {
        BounceUp();
    }

    private void BounceUp ()
    {
        if (gameObject.transform.position.y <= startPosY + threshold)
        {
            rb.AddForce(new Vector3(0, force, 0));
            rb.ResetInertiaTensor();
        }
        if (gameObject.transform.position.y < startPosY - 0.05f)
        {
            rb.AddForce(new Vector3(0, force * 2, 0));
            rb.ResetInertiaTensor();
        }
    }
}
