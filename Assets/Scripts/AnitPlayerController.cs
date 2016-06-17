using UnityEngine;
using System.Collections;

public class AnitPlayerController : MonoBehaviour {

    [Tooltip("AntiPlayer will follow this target")]
    public Transform target;
    [Tooltip("AntiPlayer movement speed")]
    public float moveSpeed = 4f;
    [Tooltip("AntiPlayer rotation speed")]
    public float rotationSpeed = 3f;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
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
