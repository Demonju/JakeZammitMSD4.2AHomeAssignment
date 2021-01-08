using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float obstacleSpeed = 0.3f;
    [SerializeField] float shotCounter;

    [SerializeField] float minTimeBetweenShots = 0.2f;

    [SerializeField] float maxTimeBetweenShots = 3f;

    [SerializeField] GameObject BulletPrefab;

    [SerializeField] float BulletSpeed = 0.3f;

    void Start()
    {
        //generate a random number
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        // every frame reduce the amount of time for shot
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0f)
        {
            EnemyFire();
            //reset ShotCounter
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void EnemyFire()
    {
        //spawn an enemy laser and Enemy Position
        GameObject enemyLaser = Instantiate(BulletPrefab, transform.position, Quaternion.identity) as GameObject;
        //shoot laser downwards: -enemyLaserSpeed
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -BulletSpeed);
    }
}
