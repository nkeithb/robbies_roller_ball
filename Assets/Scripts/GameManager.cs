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
    private GameObject levelImage;
    private GameObject antiPlayerParent;
    private GameObject player;
    internal Rigidbody rb;

  

    void Awake()
    {
        CheckInstantiation();
        DontDestroyOnLoad(gameObject);
        PlayerController.instance.GoToSpawnPoint();
        FindActiveObjects();
    }

    void InitGame()
    {
        inProgress = true;
        UserInterfaceController.instance.SetAndShowLevelText("Level " + level);
        UserInterfaceController.instance.HideLevelImageDelay(levelStartDelay);
        PlayerController.instance.GoToSpawnPoint();
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
        PlayerController.instance.GoToSpawnPoint();
        FindActiveObjects();
    }

    public void RestartGame()
    {
        level = 1;
        GoToLevel();
        PlayerController.instance.GoToSpawnPoint();
        FindActiveObjects();
    }

    public void GameOver()
    {
        UserInterfaceController.instance.SetAndShowLevelOverText("Game Over Bitch Nigga!");
        Invoke("RestartGame", levelStartDelay);
        FindActiveObjects();
    }

    private void OnLevelWasLoaded(int index)
    {
        InitGame();
        FindActiveObjects();
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
        rb = player.GetComponent<Rigidbody>();
        if (levelImage.active)
        {
            //FreezeGame();
            //player.active = false;
            antiPlayerParent.active = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        if (!levelImage.active)
        {
            //UnFreezeGame();
            //player.active = true;
            antiPlayerParent.active = true;
            rb.constraints = RigidbodyConstraints.None;

        }
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

    private void FindActiveObjects()
    {
        levelImage = GameObject.Find("LevelImage");
        antiPlayerParent = GameObject.Find("AntiPlayers");
        player = GameObject.Find("Player(Clone)");
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