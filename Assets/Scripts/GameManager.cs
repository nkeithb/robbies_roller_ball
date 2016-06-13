using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public float levelStartDelay = 2f;

    private int pickUpCount;
    private Text levelText;
    private Text levelOverText;
    private GameObject levelImage;
    private int level;

	void Awake ()
    {
        level = 1;
        CheckInstantiation();
        InitGame();
	}

    private void OnLevelWasLoaded (int index)
    {
        level++;

        InitGame();
    }

    void InitGame()
    {
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelOverText = GameObject.Find("LevelOverText").GetComponent<Text>();
        levelText.text = "Level " + level;
        levelOverText.text = "";
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
    }	
	
	void Update ()
    {
        CheckPickUpCount();
	}

    private void CheckInstantiation()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void CheckPickUpCount()
    {
        pickUpCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        if (pickUpCount == 0)
        {
            TaskCompleted();
        }
    }

    private void TaskCompleted()
    {
        levelText.text = "";
        levelOverText.text = "You collected all of the pieces!";
        levelImage.SetActive(true);
    }
}
