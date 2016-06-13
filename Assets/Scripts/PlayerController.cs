using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;
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
                count++;
                break;
            case "DontPickUp":
                other.gameObject.SetActive(false);
                count--;
                break;
            case "AntiPlayer":
                count--;
                break;
            case "DeathZone":
                count = 0;
                winText.text = "YOU LOSE!!!";
                break;
        }
        SetCountText();            
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString ();
        if (count >= 8)
        {
            winText.text = "You Have Collected All the Pieces!";
        }
    }
}