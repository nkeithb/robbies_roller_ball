using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public float levelStartDelay = 2f;

    private int pickUpCount;
    private Text levelText;
    private Text levelOverText;
    private GameObject levelImage;
    private int level;
    private int nextLevel;
    private bool inProgress;

	void Awake ()
    {
        CheckInstantiation();
        InitGame();
	}

    private void OnLevelWasLoaded (int index)
    {

        InitGame();
    }

    void InitGame()
    {
        level = 1;
        inProgress = true;
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
        if(inProgress)
        {
            CheckPickUpCount();
        }
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

    public void GameOver()
    {
        levelText.text = "";
        levelOverText.text = "Game Over Bitch Nigga!";
        levelImage.SetActive(true);
        Invoke("Restart", levelStartDelay);
    }

    private void TaskCompleted()
    {
        inProgress = false;
        levelText.text = "";
        levelOverText.text = "You collected all of the pieces!";
        levelImage.SetActive(true);
        level++;
        Invoke("GoToNextLevel", levelStartDelay);
    }

    private void GoToNextLevel()
    {
        SceneManager.LoadScene("MiniGameLvl" + level);
    }

    public void Restart()
    {
        level = 1;
        SceneManager.LoadScene("MiniGame");
    }
}
