using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public int lives, score, highScore;
    [SerializeField] private int level;
    [SerializeField] private Player player;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Spawner[] spawners;
    [SerializeField] private TextMeshProUGUI scoreText, livesText, HighScoreText;

    //private void Awake()
    //{
    //    highScore = 0;
    //    Debug.Log("highScore Awake = " + highScore);
    //}

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);

        Load();
        HighScoreText.text = "High Score : " + highScore.ToString();
        UpdateUI();
    }
    public void LoseLife()
    {
        if(lives > 0)
        {
            StartCoroutine(ReSpawn());
        }
        else
        {
           EndGame();
            
        }
    }

    void EndGame()
    {
        if(score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);

        }
        StartNewGame();
    }

    IEnumerator ReSpawn()
    {
        yield return new WaitForSeconds(2f);
        // Decrement lives
        lives--;
        // update UI lives
        UpdateUI();
        // Respawn player
        Instantiate(player.gameObject, playerSpawnPoint.position, Quaternion.identity);
        // update UI lives
    }

    public void AddPoints(int points)
    {
        score += points;
        // update UI score
        UpdateUI();
        // is the level complete?
        CheckForLevelCompletion();
    }

    private void CheckForLevelCompletion()
    {
        if(!FindAnyObjectByType<Enemy>())
        {
            foreach(Spawner spawner in spawners)
            {
                if(!spawner.completed)
                {
                    return;
                }
            }
        CompletLevel(); 
        }
    }

    void CompletLevel()
    {
        // increase level
        level++;
        Save();

        // check if there is a next level and load it if not start new game
        if(level <= SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(level);
        }
        else
        {
            EndGame();
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("lives", lives);
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("level", level);
    }

    private void Load()
    {
        lives =PlayerPrefs.GetInt("lives", 3);
        score =PlayerPrefs.GetInt("score", 0);
        level =PlayerPrefs.GetInt("level", 0);
    }

    void StartNewGame()
    {
        level = 0;
        SceneManager.LoadScene(level);
        PlayerPrefs.DeleteKey("lives");
        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.DeleteKey("level");
    }

    void UpdateUI()
    {
        scoreText.text = "score : " + score.ToString();
        livesText.text = "lives : " + lives.ToString();
    }
}
