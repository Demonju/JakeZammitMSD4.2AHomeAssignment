﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;

    [SerializeField] WaveConfig waveConfig;

    //saves the waypoint in which we want to go
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();

        //set the start position of Enemy to the 1st waypoint
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ObstacleMove();
    }

    //setting up the WaveConfig
    public void SetWaveConfig(WaveConfig waveConfigToSet)
    {
        waveConfig = waveConfigToSet;
    }

    private void ObstacleMove()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            //set the targetPosition to the waypoint where we want to go
            var targetPosition = waypoints[waypointIndex].transform.position;

            //make sure that z axis = 0
            targetPosition.z = 0f;

            var ObstacleMovement = waveConfig.GetObstacleMoveSpeed() * Time.deltaTime;

            //move Enemy from current position to targetPosition, at enemyMovement speed
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, ObstacleMovement);

            //if enemy reacher the target position
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }

        //if enemy reaches last waypoint
        else
        {
            Destroy(gameObject);
        }
    }

}
