using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public static MenuUIHandler instance;
    public TMP_InputField inputName;
    public TextMeshProUGUI bestScore;
    public string playerName;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        bestScore.text = "Best Score : " + PlayerPrefs.GetString("HighScorer") + " " + PlayerPrefs.GetInt("HighScore");
    }

    public void StartGame()
    {
        playerName = inputName.text;
        PlayerPrefs.SetString("CurrentUser", playerName);
        SceneManager.LoadScene(1);
        Debug.Log(instance.playerName);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // [System.Serializable]
    // public class SaveData
    // {
    //     public string playerName;
    // }

    // public void SaveName()
    // {
    //     SaveData data = new SaveData();
    //     data.playerName = playerName;
    // }
}
