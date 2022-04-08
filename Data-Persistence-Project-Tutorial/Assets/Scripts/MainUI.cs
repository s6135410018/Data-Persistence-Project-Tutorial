using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI inputName, show;
    private string playerName;
    private int highScore;
    // Start is called before the first frame update

    private void Awake()
    {
        if (GameScore.instance != null && show != null)
        {
            GameScore.instance.LoadDataPlayer();
            playerName =  GameScore.instance.playerName;
            highScore = GameScore.instance.score;
            show.text = "Player : " + playerName + " Highscore : " + highScore;
        }
        else
        {
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (inputName != null)
        {
            GameScore.instance.activePlayer = inputName.text;
        }
    }
    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameScore.instance.SaveDataPlayer();
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
            GameScore.instance.SaveDataPlayer();
#endif
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
