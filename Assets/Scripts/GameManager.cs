using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int lives, score;
    [SerializeField] private int level;
    [SerializeField] private Player player;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Spawner[] spawners;


    private void Start()
    {
        Load();
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
        if(score > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", score);
        }
        StartNewGame();
    }

    IEnumerator ReSpawn()
    {
        yield return new WaitForSeconds(2f);
        // Decrement lives
        lives--;
        // Respawn player
        Instantiate(player.gameObject, playerSpawnPoint.position, Quaternion.identity);
        // update UI lives
    }

    public void AddPoints(int points)
    {
        score += points;
        // update UI score
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
        Save();
        // increase level
        level++;
        // check if there is a next level and load it if not start new game
        if(SceneManager.sceneCountInBuildSettings > level)
        {
            SceneManager.LoadScene(level);
        }
        else
        {
            StartNewGame();
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
}
