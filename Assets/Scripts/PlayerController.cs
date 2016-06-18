using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float jumpForce = 700.0f;
    public float jumpCooldown = 0.01f;
    public Text countText;
    public GameObject Button;
    public float teleportDelay = 3.0f;
 

    public AudioClip[] pickUpSounds;
    public AudioClip[] deathSounds;
    public AudioClip antiPlayerSound;
    public AudioClip dontPickUpSound;
    public AudioClip wallSound;
    public AudioClip obstacleSound;
    public AudioClip[] rampSounds;
    public AudioClip[] jumpSounds;
    public AudioClip[] teleportSounds;
    public AudioClip[] hammerSounds;
    public AudioClip[] completionSounds;

    public static PlayerController instance = null;

    private Rigidbody rb;
    private static int count = 0;
    private UserInterfaceController userInterfaceController;
    private Transform playerTransform;
    private Transform spawnPoint;
    private Rigidbody rigidBody;
    private bool powerUp = false;
    private bool speedUp = false;
    private TrailRenderer playerTrail;

    internal bool recentlyJumped = false;
    internal bool recentlyTeleported;
    internal int scoreMultiplier = 1;

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
                count += 10 * scoreMultiplier;
                other.gameObject.SetActive(false);
                SoundManager.instance.RandomizeSfx(pickUpSounds);
                break;
            case "Speed Pick Up":
                count += 35 * scoreMultiplier;
                other.gameObject.SetActive(false);
                SoundManager.instance.RandomizeSfx(pickUpSounds);
                SpeedUp();
                Invoke("ResetSpeedRatio", 10.0f);
                break;
            case "Pick Up High":
                count += 20 * scoreMultiplier;
                other.gameObject.SetActive(false);
                SoundManager.instance.RandomizeSfx(pickUpSounds);
                break;
            case "DontPickUp":
                other.gameObject.SetActive(false);
                SoundManager.instance.PlaySingle(dontPickUpSound);
                count += 50 * scoreMultiplier;
                PowerUp();
                Invoke("ResetScoreRatio", 10.0f);
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
            case "Hammer":
                HammerSmack();
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
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.rb.constraints == RigidbodyConstraints.None 
            && recentlyJumped == false)
        {
            recentlyJumped = true;
            rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f));
            SoundManager.instance.RandomizeSfx(jumpSounds);
            Invoke("SetRecentlyJumped", jumpCooldown);
        }
        if (Input.GetKeyDown(KeyCode.M))
            print(string.Format("Sign X : {0:0.00##}", GameManager.instance.inProgress));


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

    public void HammerAPScore()
    {
        count += 100;
        UserInterfaceController.instance.SetAndShowCountText(count);
    }
    private void HammerSmack()
    {
        //Return either 1 or -1
        int signOne = (Random.Range(0, 2) * 2) - 1;
        int signTwo = (Random.Range(0, 2) * 2) - 1;
        //Multiply each sign by a random number between 10000 and 25000
        float dirX = Random.Range(10000f, 25000f) * signOne;
        float dirZ = Random.Range(10000f, 25000f) * signTwo;
        //Add forces calculated above to the player on their respective axes
        rb.AddForce(new Vector3(dirX, 3000.0f, dirZ));
        SoundManager.instance.RandomizeSfx(hammerSounds);
        //Automatic death after hammer hit
        //Invoke("DeathCheck", 1.5f);
    }

    private void ResetScoreRatio()
    {
        scoreMultiplier = 1;
        powerUp = false;
        UserInterfaceController.instance.HidePowerUpText();
    }
    private void ResetSpeedRatio()
    {
        speed = 15;
        speedUp = false;
        UserInterfaceController.instance.HideSpeedUpText();
    }

    private void PowerUp()
    {
        // Place power up for DPU object here
        scoreMultiplier = 5;
        UserInterfaceController.instance.ShowPowerUpText();
        powerUp = true;
    }
    private void SpeedUp()
    {
        // Place power up for DPU object here
        speed = 30;
        UserInterfaceController.instance.ShowSpeedUpText();
        speedUp = true;
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
        if (count > 0 && powerUp == false)
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

    private void SetRecentlyJumped()
    {
        recentlyJumped = false;
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
        playerTrail.Clear();
    }    

    internal void SetTransformValues(string spawnTag)
    {   
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerTrail = GameObject.FindGameObjectWithTag("Player").GetComponent<TrailRenderer>();
        spawnPoint = GameObject.Find(spawnTag).transform;
        rigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }
}