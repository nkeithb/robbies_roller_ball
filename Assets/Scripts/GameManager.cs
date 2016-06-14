using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public float levelStartDelay = 2f;
    public static int level = 1;

    private int pickUpCount;
    private int nextLevel;
    private bool inProgress;

    private bool runThrough = true;

	void Awake ()
    {
        CheckInstantiation();
        DontDestroyOnLoad(gameObject);
        if (!runThrough)
        {
            InitGame();
        }
    }

    private void OnLevelWasLoaded (int index)
    {
        InitGame();
    }

    void InitGame()
    {
        inProgress = true;
        UserInterfaceController.instance.SetAndShowLevelText(level);
        UserInterfaceController.instance.HideLevelImageDelay(levelStartDelay);
    }

    void Update()
    {
        if (inProgress)
        {
            CheckPickUpCount();
            // enable player/AP scripts
            
            
        }
        else if (!inProgress)
        { 
            //disable player/AP scripts
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
        inProgress = false;
        UserInterfaceController.instance.SetAndShowLevelOverText("Game Over Bitch Nigga!");
        Invoke("RestartGame", levelStartDelay);
        inProgress = true;
    }

    private void TaskCompleted()
    {
        inProgress = false;
        UserInterfaceController.instance.SetAndShowLevelOverText("You collected all of the pieces!");
        level++;
        Invoke("GoToLevel", levelStartDelay);
        inProgress = true;
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
        if (Input.GetKeyDown(KeyCode.R) && inProgress)
            TaskCompleted();
    }
}
