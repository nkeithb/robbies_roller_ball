﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour {

    public static UserInterfaceController instance;

    private Text levelText;
    private Text levelOverText;
    private GameObject levelImage;
    private GameObject pauseText;

    void Awake()
    {
        CheckInstantiation();
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        pauseText = GameObject.Find("PauseText");
        levelOverText = GameObject.Find("LevelOverText").GetComponent<Text>();
        pauseText.SetActive(false);
    }

    public void SetAndShowLevelText(string level)
    {
        levelText.text = level;
        levelOverText.text = "";
        levelImage.SetActive(true);
    }

    public void ShowPauseText()
    {
        pauseText.SetActive(true);
    }

    public void HidePauseText()
    {
        pauseText.SetActive(false);
    }

    public void SetAndShowLevelOverText(string textToUse)
    {
        levelText.text = "";
        levelOverText.text = textToUse;
        levelImage.SetActive(true);
    }

    public void HideLevelImage()
    {
        levelImage.SetActive(false);
    }

    public void HideLevelImageDelay(float timeDelay)
    {
        Invoke("HideLevelImage", timeDelay);
    }

    private void CheckInstantiation()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
