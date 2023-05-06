using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burner2 : MonoBehaviour
{
    public ParticleSystem finalFire;

    private void Start()
    {
        // Set the particle system to be initially invisible
        finalFire.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("oil"))
        {
            // Play the particle system to make it visible
            finalFire.Play();
        }
    }
}


