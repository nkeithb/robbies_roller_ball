using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float jumpForce = 250.0f - 250.0f;
    public Text countText;
    public GameObject Button;
    public float teleportDelay = 3.0f;
    public bool recentlyTeleported;

    public AudioClip[] pickUpSounds;
    public AudioClip[] deathSounds;
    public AudioClip antiPlayerSound;
    public AudioClip dontPickUpSound;
    public AudioClip wallSound;
    public AudioClip obstacleSound;
    public AudioClip[] rampSounds;
    public AudioClip[] jumpSounds;
    public AudioClip[] teleportSounds;

    public static PlayerController instance = null;

    private Rigidbody rb;
    private static int count = 0;
    private UserInterfaceController userInterfaceController;
    private Transform playerTransform;
    private Transform spawnPoint;
    private Rigidbody rigidBody;

    //Sets base information for all Variables at the start of the run.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void FixedUpdate()
    {
        CheckPlayerInputs();
    }

    //Checks for collision with game items. 
    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Pick Up":
                //CheckMass();
                count += 100;
                other.gameObject.SetActive(false);
                SoundManager.instance.RandomizeSfx(pickUpSounds);
                break;
            case "DontPickUp":
                other.gameObject.SetActive(false);
                SoundManager.instance.PlaySingle(dontPickUpSound);
                count--;
                break;
            case "AntiPlayer":
                //rb.mass -= 0.01f
                SoundManager.instance.PlaySingle(antiPlayerSound);
                CheckCount();
                break;
            case "Wall":
                SoundManager.instance.PlaySingle(wallSound);
                break;
            case "Obstacle":
                SoundManager.instance.PlaySingle(obstacleSound);
                break;
            case "Ramp":
                SoundManager.instance.RandomizeSfx(rampSounds);
                break;
            case "DeathZone":
                DeathCheck();
                break;
        }
        if (other.name.Contains("Teleporter") && recentlyTeleported == false)
        {
            SoundManager.instance.RandomizeSfx(teleportSounds);
            Teleport(other.tag);
            AddTeleportDelay();
        }
        UserInterfaceController.instance.SetAndShowCountText(count);
    }

    // Checks for input from keyboard to determine user actions
    void CheckPlayerInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.rb.constraints == RigidbodyConstraints.None)
        {
            rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f));
            SoundManager.instance.RandomizeSfx(jumpSounds);
        }


        //else if (Input.GetKeyUp(KeyCode.Space))
        //rb.AddForce(new Vector3(0.0f, -jumpForce, 0.0f));


        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    public void GoToSpawnPoint()
    {
        Teleport("Spawn Point");
    }

    private void DeathCheck()
    {
        if (GameManager.instance.rb.constraints == RigidbodyConstraints.None)
        {
            SoundManager.instance.RandomizeSfx(deathSounds);
            count = 0;
            GameManager.instance.GameOver();
        }
    }

    private void CheckCount()
    {
        if (count > 0)
            count--;
    }

    private void CheckMass()
    {
        if (rb.mass < 1.0)
            rb.mass += 0.1f;
        else if (rb.mass < 0.02)
            rb.mass = 0.02f;
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
        rigidBody.velocity = new Vector3(0, 0, 0);
        rigidBody.ResetInertiaTensor();
        playerTransform.position = spawnPoint.position;
    }    

    internal void SetTransformValues(string spawnTag)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPoint = GameObject.Find(spawnTag).transform;
        rigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }
}