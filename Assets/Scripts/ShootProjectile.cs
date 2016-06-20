using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour {

    public Transform projectile;
    public float bulletSpeed = 200.0f;


	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Put this in your update function
        if (Input.GetButtonDown("Fire1"))
        {
            // Instantiate the projectile at the position and rotation of this transform
            Transform clone;
            clone = Instantiate(projectile, transform.position, transform.rotation) as Transform;
            Collider cloneCollider = clone.GetComponent<Collider>();
            Collider myCollider = GetComponent<Collider>();
            // Add force to the cloned object in the object's forward direction
            Physics.IgnoreCollision(myCollider, cloneCollider);
            clone.GetComponent<Rigidbody>().AddForce(clone.transform.forward * bulletSpeed);
            //Destroy (clone, 20.0f);
        }
    }
}
