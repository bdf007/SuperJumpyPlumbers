using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int lives, score;
    [SerializeField] private Player player;
    [SerializeField] private Transform playerSpawnPoint;

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
    }
}
