using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	
	void Update () {

        transform.Rotate (new Vector3 (90,45,15) * Time.deltaTime);
	}
}
