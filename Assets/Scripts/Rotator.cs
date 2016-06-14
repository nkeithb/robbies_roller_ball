using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public Vector3 rotationSpeed = new Vector3 (90f, 45f, 15f);

    void Update ()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
