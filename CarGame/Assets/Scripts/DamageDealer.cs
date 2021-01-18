using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] AudioClip obstacleDestroyed;
    [SerializeField] [Range(0, 1)] float obstacleDestroyedVolume = 0.30f;

    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionDuration = 1f;

    //returns the amount of damage
    public int GetDamage()
    {
        return damage;
    }

    //destroys the gameObject
    public void Hit()
    {
        Destroy(this.gameObject);

        if (obstacleDestroyed != null)
            AudioSource.PlayClipAtPoint(obstacleDestroyed, Camera.main.transform.position, obstacleDestroyedVolume);

        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
    }
}
