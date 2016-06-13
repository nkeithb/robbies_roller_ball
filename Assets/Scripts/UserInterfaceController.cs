using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour {

    public static UserInterfaceController instance;

    private Text levelText;
    private Text levelOverText;
    private GameObject levelImage;

    void Awake()
    {
        instance = this;
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelOverText = GameObject.Find("LevelOverText").GetComponent<Text>();
    }

    public void SetAndShowLevelText(int level)
    {
        levelText.text = "Level " + level;
        levelOverText.text = "";
        levelImage.SetActive(true);
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
}
