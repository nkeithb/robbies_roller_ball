﻿using UnityEngine;
using System.Collections;

public class Bouncer : MonoBehaviour {

    [Tooltip("Bounce force")]
    public float force = 90.0f;
    [Tooltip("Minimum distance from starting position to trigger a bounce")]
    public float threshold = 0.2f;

    private Vector3 startPos;
    private float startPosY;

    void Start ()
    {
        startPosY = transform.position.y;
    }
	
	void FixedUpdate ()
    {
        BounceUp();
    }

    private void BounceUp ()
    {
        if (transform.position.y <= startPosY + threshold)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, force, 0));
            GetComponent<Rigidbody>().ResetInertiaTensor();
        }
        if (transform.position.y < startPosY - 0.05f)
        {
            GetComponent<Rigidbody>().ResetInertiaTensor();
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().AddForce(new Vector3(0, force * Random.Range(-1f,1f), 0));
        }
    }
}