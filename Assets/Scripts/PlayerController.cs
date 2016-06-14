using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float jumpForce = 250.0f - 250.0f;
    public Text countText;
    public GameObject Button;

    public AudioClip[] pickUpSounds;
    public AudioClip[] deathSounds;
    public AudioClip antiPlayerSound;
    public AudioClip dontPickUpSound;
    public AudioClip wallSound;
    public AudioClip obstacleSound;
    public AudioClip[] rampSounds;
    public AudioClip[] jumpSounds;

    private Rigidbody rb;
    bool script;
    private static int count = 0;

    //Sets base information for all Variables at the start of the run.
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        
    }
    // Checks for input from keyboard to determine Horizontal and Vertical movement of "Player"

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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

        //Checks for collision with game items. 
        void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Pick Up":
                other.gameObject.SetActive(false);
                SoundManager.instance.RandomizeSfx(pickUpSounds);
                count++;
                break;
            case "DontPickUp":
                other.gameObject.SetActive(false);
                SoundManager.instance.PlaySingle(dontPickUpSound);
                count--;
                break;
            case "AntiPlayer":
                SoundManager.instance.PlaySingle(antiPlayerSound);
                count--;
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
                SoundManager.instance.RandomizeSfx(deathSounds);
                count = 0;
                GameManager.instance.GameOver();
                break;
        }
        SetCountText();
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}