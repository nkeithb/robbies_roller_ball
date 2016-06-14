using UnityEngine;
using System.Collections;

public class AnitPlayerController : MonoBehaviour {

    public Transform target;
    public int moveSpeed = 6;
    public int rotationSpeed = 3;
    
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

   void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            dir.z = 0.0f;
            if (dir != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.FromToRotation(Vector3.right, dir),
                    rotationSpeed * Time.deltaTime);
                 transform.position += (target.position - transform.position).normalized
                * moveSpeed * Time.deltaTime;
        }
    }
   
}
