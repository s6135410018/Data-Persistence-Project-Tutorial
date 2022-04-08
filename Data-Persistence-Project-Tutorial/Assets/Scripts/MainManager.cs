using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public Button menuButton;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText, activePlayer, player, highScore;
    public GameObject GameOverText;
    private string playerName;
    private bool m_Started = false;
    private int m_Points, highscore;

    private bool m_GameOver = false;

    private void Awake()
    {
        if (GameScore.instance != null)
        {
            GameScore.instance.LoadDataPlayer();
            activePlayer.text = "Player :  " + GameScore.instance.activePlayer;
            player.text = "BestPlayer : " + GameScore.instance.playerName;
            highScore.text = "Highscore : "  + GameScore.instance.score;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
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
        menuButton.gameObject.SetActive(true);
        if (m_Points > GameScore.instance.score)
        {
            GameScore.instance.score = m_Points;
            GameScore.instance.playerName = GameScore.instance.activePlayer;
            GameScore.instance.SaveDataPlayer();
        }
    }
        
}
