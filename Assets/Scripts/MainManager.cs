using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;


public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI currentUserText;

    private bool m_Started = false;
    private int m_Points;
    public int score;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        highscoreText.text = "BestScore : " + PlayerPrefs.GetString("HighScorer") + " : " + PlayerPrefs.GetInt("HighScore");
        CurrentUser();
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {222,222,222,222,225,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        highscoreText.text = "BestScore : " + PlayerPrefs.GetString("HighScorer") + " : " + PlayerPrefs.GetInt("HighScore");
        UpdateScore();
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {           
            if (Input.GetKeyDown(KeyCode.Space))
            {    
                UpdateScore();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }


    private void UpdateScore()
    {  
        if (m_Points > PlayerPrefs.GetInt("HighScore", 0))   // zero is default value;
        {
            PlayerPrefs.SetInt("HighScore",m_Points);
            PlayerPrefs.SetString("HighScorer",MenuUIHandler.instance.playerName);        
        }
    }
    void CurrentUser()
    {
        currentUserText.text = "Current User : " + PlayerPrefs.GetString("CurrentUser");
    }

}

