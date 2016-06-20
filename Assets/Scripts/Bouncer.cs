using UnityEngine;
using System.Collections;

public class Bouncer : MonoBehaviour {

    [Tooltip("Bounce force")]
    public float force = 90.0f;
    [Tooltip("Minimum distance from starting position to trigger a bounce")]
    public float threshold = 0.2f;

    public bool randomBouncing = true;

    private Vector3 startPos;
    private float startPosY;
    private float multiplier;

    void Start ()
    {
        startPosY = transform.position.y;
        //SetMultiplier();
    }
	
	void FixedUpdate ()
    {
        BounceUp();
    }

    private void SetMultiplier()
    {
        if (randomBouncing)
            multiplier = Random.Range(.75f, 1.25f);
        else if (!randomBouncing)
            multiplier = 1.0f;
    }

    private void BounceUp ()
    {
        SetMultiplier();
        if (transform.position.y <= startPosY + threshold)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, force, 0));
            GetComponent<Rigidbody>().ResetInertiaTensor();
        }
        if (transform.position.y < startPosY - 0.05f)
        {
            GetComponent<Rigidbody>().ResetInertiaTensor();
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().AddForce(new Vector3(0, force * multiplier, 0));
        }
    }
}
