using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float valueX = 90f;
    public float valueY = 45f;
    public float valueZ = 15f;

    void Update () {

        transform.Rotate (new Vector3 (valueX,valueY,valueZ) * Time.deltaTime);
	}
}
