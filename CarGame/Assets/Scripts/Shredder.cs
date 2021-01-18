using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    [SerializeField] int scoreValue = 5;

    //runs whenever otherObject triggers the Shredder
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        Destroy(otherObject.gameObject);

        if (!(otherObject.gameObject.tag == "Bullet"))
        {
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
        }
    }
}
