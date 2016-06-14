using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    public Text countText;
    public Text winText;
    public GameObject Button;

    public AudioClip pickUpSound;
    public AudioClip deathSound;
    public AudioClip antiPlayerSound;
    public AudioClip dontPickUpSound;
    public AudioClip wallSound;
    public AudioClip obstacleSound;
    public AudioClip rampSound1;
    public AudioClip rampSound2;
    public AudioClip rampSound3;

    private Rigidbody rb;
    private static int count;

    //Sets base information for all Variables at the start of the run.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }
    // Checks for input from keyboard to determine Horizontal and Vertical movement of "Player"

    void FixedUpdate()
    {
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
                SoundManager.instance.PlaySingle(pickUpSound);
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
                SoundManager.instance.RandomizeSfx([rampSound1, rampSound2, rampSound3]);
                break;
            case "DeathZone":
                SoundManager.instance.PlaySingle(deathSound);
                count = 0;
                winText.text = "YOU LOSE!!!";
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