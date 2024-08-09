using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckStatsEnemy : MonoBehaviour
{
    private GameObject enemy;
    private EnemyFighter enemyFighter;
    void Update()
    {
        if (enemy == null)
        {
            enemy = GameObject.FindWithTag("Enemy");
            if (enemy == null)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    if(enemy != null)
    { 
        enemyFighter = enemy.GetComponent<EnemyFighter>(); 
    }
    }
}

