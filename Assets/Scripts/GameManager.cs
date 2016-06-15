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
    private bool paused = false;

	void Awake ()
    {
        CheckInstantiation();
        FreezeGame();
        DontDestroyOnLoad(gameObject);
    }

    void InitGame()
    {
        
        FreezeGame();
        UserInterfaceController.instance.SetAndShowLevelText(level);
        Invoke("UnPauseGame", levelStartDelay);
    }

    void Update()
    {
        if (inProgress)
        {
            CheckPickUpCount();              
        }
        CheckPlayerInputs();
    }

    public void GameOver()
    {
        PauseGame();
        UserInterfaceController.instance.SetAndShowLevelOverText("Game Over Bitch Nigga!");
        Invoke("RestartGame", levelStartDelay);
    }    

    public void RestartLevel()
    {
        GoToLevel();
    }

    public void RestartGame()
    {
        PauseGame();
        level = 1;
        GoToLevel();
    }

    //Private Functions

    private void CheckInstantiation()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void CheckPickUpCount()
    {
        pickUpCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        if (pickUpCount == 0)
        {
            TaskCompleted();
        }
    }

    private void TaskCompleted()
    {
        inProgress = false;
        UserInterfaceController.instance.SetAndShowLevelOverText("You collected all of the pieces!");
        level++;
        Invoke("GoToLevel", levelStartDelay);
    }

    private void GoToLevel()
    {
        SceneManager.LoadScene("MiniGameLvl" + level);
    }

    private void CheckPlayerInputs()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !inProgress && level == 1)
            InitGame();
        if (Input.GetKeyDown(KeyCode.R) && inProgress)
            TaskCompleted();
        if (Input.GetKeyDown(KeyCode.Escape))
            AttemptPauseGame();
    }

    private void AttemptPauseGame()
    {
        if(!paused)
        {
            PauseGame();
        }
        UnPauseGame();
    }

    private void PauseGame()
    {
        FreezeGame();
        paused = true;
        UserInterfaceController.instance.SetAndShowLevelText(level);
    }

    private void UnPauseGame()
    {
        paused = false;
        UserInterfaceController.instance.HideLevelImage();
        UnFreezeGame();
    }

    private void FreezeGame()
    {
        Time.timeScale = 0.0f;
    }

    private void UnFreezeGame()
    {
        Time.timeScale = 1.0f;
    }
}
