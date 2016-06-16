using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

    // This script is currently unused, however teleporter functionality may be able to be moved in to this class
    // It could also be changed to accept a public GameObject in-editor as the destination to add functionality

    public GameObject destination;
    public float teleportDelay = 0.1f;
    private bool recentlyTeleported = false;
    private Transform playerTransform;
    private Transform spawnPoint;
    private Rigidbody rigidBody;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        // Here, 'Teleporter1' will hold tag 'Teleporter2' and vice versa, creating the teleport effect. 
        // Can be used multiple times, however the tag of the teleporter MUST match the name of the 
        // target teleporter.
        if (other.name.Contains("Player") && recentlyTeleported == false)
        {
            SoundManager.instance.RandomizeSfx(PlayerController.instance.teleportSounds);
            Teleport(destination.tag);
            AddTeleportDelay();
        }
    }

    private void SetRecentlyTeleported()
    {
        recentlyTeleported = false;
    }

    private void AddTeleportDelay()
    {
        recentlyTeleported = true;
        Invoke("SetRecentlyTeleported", teleportDelay);
    }

    private void Teleport(string spawnTag)
    {
        SetTransformValues(spawnTag);
        playerTransform.position = spawnPoint.position;
        rigidBody.velocity = new Vector3(0, 0, 0);
        rigidBody.ResetInertiaTensor();
    }

    private void SetTransformValues(string spawnTag)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPoint = GameObject.Find(spawnTag).transform;
        rigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }
}
