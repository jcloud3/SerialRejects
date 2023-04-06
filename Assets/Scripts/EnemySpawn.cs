using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemies;
    public int maxEnemies = 4;
 
    public int startSpawnTime = 10;
    public int spawnTime = 5;
    private int enemyCount = 0;
 
 
    // Use this for initialization
    void Start () {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating ("Spawn", startSpawnTime, spawnTime);
    }
 
    // Update is called once per frame
    void Update () {
     
    }
 
    void Spawn () {
        // Find a random index between zero and one less than the number of spawn points.
        if (enemyCount<maxEnemies){
            int spawnPoint = Random.Range (0, spawnPoints.Length);
            int randomEnemy = Random.Range (0, enemies.Length);
            Debug.Log("Spawn");
            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(enemies[randomEnemy],spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation);
            enemyCount += 1;
        }
        
    }
 
}
