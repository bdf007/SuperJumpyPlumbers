using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
                Hurt();
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                StartCoroutine(HurtEnemy(collision.gameObject.GetComponent<Enemy>()));
                break;
            case "coin":
                // add to score
                break;
        }
    }

    IEnumerator HurtEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        yield return new WaitForEndOfFrame();
        // add points to score based on enemy point value
        // add points will check to see if any enemies are left, complete level?
    }

    void Hurt()
    {
        Destroy(this.gameObject);
    }
}
