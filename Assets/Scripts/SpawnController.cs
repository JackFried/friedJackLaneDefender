using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMaster : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public GameObject SpawnPoint3;
    public GameObject SpawnPoint4;
    public GameObject SpawnPoint5;
    public float TimerMax;

    private float timer;
    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        //Setting up timer
        timer = TimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        //Timer is used to delay enemy spawns
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            RandomizeEnemy();
            //Enemy spawn location is randomized between five points
            int randSpawn = Random.Range(0, 5);
            if (randSpawn == 0)
            {
                Instantiate(enemy, SpawnPoint1.transform.position, Quaternion.identity);
            }
            else if (randSpawn == 1)
            {
                Instantiate(enemy, SpawnPoint2.transform.position, Quaternion.identity);
            }
            else if (randSpawn == 2)
            {
                Instantiate(enemy, SpawnPoint3.transform.position, Quaternion.identity);
            }
            else if (randSpawn == 3)
            {
                Instantiate(enemy, SpawnPoint4.transform.position, Quaternion.identity);
            }
            else if (randSpawn == 4)
            {
                Instantiate(enemy, SpawnPoint5.transform.position, Quaternion.identity);
            }

            timer = TimerMax;
        }
    }

    private void RandomizeEnemy() //Changes which enemy will be spawned next
    {
        int randSpawn = Random.Range(0, 3);
        if (randSpawn == 0)
        {
            enemy = Enemy1;
        }
        else if (randSpawn == 1)
        {
            enemy = Enemy2;
        }
        else if (randSpawn == 2)
        {
            enemy = Enemy3;
        }
    }

}
