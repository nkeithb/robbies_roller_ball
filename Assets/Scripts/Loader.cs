using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameObject gameManager;
    public GameObject soundManager;
    public GameObject playerController;
    public GameObject userInterfaceController;

	void Awake ()
    {
        if (PlayerController.instance == null)
            Instantiate(playerController);
        if (GameManager.instance == null)
            Instantiate(gameManager);
        if (SoundManager.instance == null)
            Instantiate(soundManager);
        if (UserInterfaceController.instance == null)
            Instantiate(userInterfaceController);
    }
	
}
