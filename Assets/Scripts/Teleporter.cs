using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

    /* - Will teleport to destination object on collision with player
       - teleportDelay > 0 required to prevent looping
       - Replace PlayerController.instance.recentlyTeleported with
         location of your own recentlyTeleported bool. */

    [Tooltip("Destination GameObject")]
    public GameObject destination;
    [Tooltip("Toggle teleporter sounds")]
    public bool soundEnabled = true;
    [Tooltip("Time in seconds that teleportation is prevented after using this teleporter")]
    public float teleportDelay = 0.1f;
    [Tooltip("Sets velocity to 0 upon teleportation if true")]
    public bool stopMotionAfterTeleport = true;

    private Transform playerTransform;
    private Transform spawnPoint;
    private Rigidbody rigidBody;
    private GameObject player;
    private TrailRenderer trail;

    // Use this for initialization
	void Start ()
    {
        PlayerController.instance.recentlyTeleported = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        // Player(Clone) will trigger the Teleport method, which always moves 
        // the player to the destination GameObject, if teleport is not on cd.

        if (other.name == "Player(Clone)" && PlayerController.instance.recentlyTeleported == false)
        {
            PlayTeleporterSound();
            AddTeleportDelay();
            Teleport(destination.name);
        }
    }

    private void PlayTeleporterSound()
    {
        if (soundEnabled == true)
            SoundManager.instance.RandomizeSfx(PlayerController.instance.teleportSounds);
    }

    private void SetRecentlyTeleported()
    {
        PlayerController.instance.recentlyTeleported = false;
    }

    private void AddTeleportDelay()
    {
        PlayerController.instance.recentlyTeleported = true;
        Invoke("SetRecentlyTeleported", teleportDelay);
    }

    private void Teleport(string spawnTag)
    {
        SetTransformValues(spawnTag);
        playerTransform.position = spawnPoint.position;
        if (stopMotionAfterTeleport)
        {
            rigidBody.velocity = new Vector3(0, 0, 0);
            rigidBody.ResetInertiaTensor();
            trail.Clear();
        }
    }

    private void SetTransformValues(string spawnTag)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        trail = player.GetComponent<TrailRenderer>();
        playerTransform = player.transform;
        spawnPoint = GameObject.Find(spawnTag).transform;
        rigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }
}
