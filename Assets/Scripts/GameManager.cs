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
    private GameObject levelImage;

    private Transform playerTransform;
    private Transform spawnPoint;

    void Awake()
    {
        CheckInstantiation();
        DontDestroyOnLoad(gameObject);
        GoToSpawnPoint();
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
        RunCheck();
    }

    public void RestartLevel()
    {
        GoToLevel();
    }

    public void RestartGame()
    {
        level = 1;
        GoToLevel();
        GoToSpawnPoint();
    }

    public void GameOver()
    {
        UserInterfaceController.instance.SetAndShowLevelOverText("Game Over Bitch Nigga!");
        Invoke("RestartGame", levelStartDelay);
    }

    private void GoToSpawnPoint()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn Point").transform;

        playerTransform.position = spawnPoint.position;
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
        // Go to 
    }

    private void RunCheck()
    {
        levelImage = GameObject.Find("LevelImage");
        //if (levelImage.active)
        //    FreezeGame();
        //if (!levelImage.active && !paused)
        //    UnFreezeGame();
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
        UserInterfaceController.instance.ShowPauseText();
    }

    private void UnPauseGame()
    {
        UnFreezeGame();
        paused = false;
        UserInterfaceController.instance.HidePauseText();
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