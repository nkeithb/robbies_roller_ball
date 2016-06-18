using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CollisionSceneLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                SceneManager.LoadScene(gameObject.tag + "s_Fun_Zone");
                SoundManager.instance.RandomizeSfx(PlayerController.instance.teleportSounds);
                break;
        }
    } 
}
