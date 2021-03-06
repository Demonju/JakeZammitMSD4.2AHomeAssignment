﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using System.Threading;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] int health = 50;

    [SerializeField] AudioClip playerDestroyed;
    [SerializeField] [Range(0, 1)] float playerDestroyedVolume = 0.75f;

    [SerializeField] int scoreValue = 5;

    float xMin, xMax, yMin, yMax;

    float padding = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Boundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public int GetHealth()
    {
        return health;
    }

    private void Boundaries()
    {
        Camera gameCamera = Camera.main;
        //xMin = 0 according to Camera view
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        //var changes its variable type
        //depending on what i save it in
        //deltaX will have the difference in the x-axis which the player moves
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        //newXPos = current x-position     + difference in x
        var newXPos = transform.position.x + deltaX;
        //clamp the ship between xMin and xMax
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);

        //the above in y axis:
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        var newYPos = transform.position.y + deltaY;
        //clamp the ship between yMin and yMax
        newYPos = Mathf.Clamp(newYPos, yMin, yMax);

        //move the Player ship to the newXPos
        this.transform.position = new Vector2(newXPos, newYPos);
    }

    //reduces health whenever the enemy collides with a gameObject
    //which has a DamageDealer component
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        //access the DamageDealer class from "otherObject" which hits enemy
        //and reduces health accordingly
        DamageDealer dmgDealer = otherObject.gameObject.GetComponent<DamageDealer>();

        ProcessHit(dmgDealer);
    }

    //Whenever ProcessHit() is called, send us the DamageDealer details
    private void ProcessHit(DamageDealer dmgDealer)
    {
        health -= dmgDealer.GetDamage();
        dmgDealer.Hit();

        AudioSource.PlayClipAtPoint(playerDestroyed, Camera.main.transform.position, playerDestroyedVolume);
     //print (health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        if (scoreValue >= 100)
        {
            FindObjectOfType<Level>().LoadWinner();
        }
        else
        {
            //find object of type level in the hierarchy and run the method LoadGameOver()
            FindObjectOfType<Level>().LoadGameOver();
        }
    }

}
