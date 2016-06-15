using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public float levelStartDelay = 2f;
    public static int level = 1;

    private int pickUpCount;
    private int nextLevel;
    private bool inProgress;
    private bool paused = false;
    private bool runThrough = true;

    void Awake()
    {
        CheckInstantiation();
        DontDestroyOnLoad(gameObject);
        if (!runThrough)
        {
            InitGame();
        }
    }

    void InitGame()
    {
        inProgress = true;
        UserInterfaceController.instance.SetAndShowLevelText("Level " + level);
        UserInterfaceController.instance.HideLevelImageDelay(levelStartDelay);
    }

    void Update()
    {
        if (inProgress)
        {
            CheckPickUpCount();
        }
        CheckPlayerInputs();
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

    public void GameOver()
    {
        UserInterfaceController.instance.SetAndShowLevelOverText("Game Over Bitch Nigga!");
        Invoke("RestartGame", levelStartDelay);
    }

    private void OnLevelWasLoaded(int index)
    {
        InitGame();
    }

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
        //switch(Input.inputString)
        //  case:
        if (Input.GetKeyDown(KeyCode.Return) && !inProgress && level == 1)
            InitGame();
        if (Input.GetKeyDown(KeyCode.R) && inProgress)
            TaskCompleted();
        if (Input.GetKeyDown(KeyCode.Escape))
            AttemptPauseGame();
    }

    private void AttemptPauseGame()
    {
        if (!paused)
            PauseGame();
        else if (paused)
            UnPauseGame();
    }

    private void PauseGame()
    {
        paused = true;
        FreezeGame();
        UserInterfaceController.instance.SetAndShowLevelText("Paused");
    }

    private void UnPauseGame()
    {
        UnFreezeGame();
        paused = false;
        UserInterfaceController.instance.HideLevelImage();
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