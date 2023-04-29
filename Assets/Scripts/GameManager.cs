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
    private Spawner[] spawners;

    public void LoseLife()
    {
        if(lives > 0)
        {
            StartCoroutine(ReSpawn());
        }
        else
        {
            // Game over
            
        }
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
        }
    }

    void CompletLevel()
    {
        // increase level
        level++;
        // check if there is a next level and load it
        if (SceneManager.GetSceneAt(level) != null)
        {
            SceneManager.LoadScene(level);
        }
        else
        {
            // game over
            Debug.Log("Game Won! Nice!");
        }


    }
}
