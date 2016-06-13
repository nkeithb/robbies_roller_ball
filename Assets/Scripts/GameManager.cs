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
    private static int level = 1;
    private int nextLevel;
    private bool inProgress;

	void Awake ()
    {
        CheckInstantiation();
        //InitGame();
	}

    private void OnLevelWasLoaded (int index)
    {
        InitGame();
    }

    void InitGame()
    {
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
        CheckPlayerInputs();
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
        Invoke("RestartGame", levelStartDelay);
    }

    private void TaskCompleted()
    {
        inProgress = false;
        levelText.text = "";
        levelOverText.text = "You collected all of the pieces!";
        levelImage.SetActive(true);
        level++;
        Invoke("GoToLevel", levelStartDelay);
    }

    private void GoToLevel()
    {
        SceneManager.LoadScene("MiniGameLvl" + level);
    }

    public void RestartLevel()
    {
        GoToLevel();
    }

    public void RestartGame()
    {
        level = 1;
        GoToLevel();
    }

    private void CheckPlayerInputs()
    {
        //switch(Input.inputString)
          //  case:
        if (Input.GetKeyDown(KeyCode.Return) && !inProgress && level == 1)
            InitGame();
    }
}
