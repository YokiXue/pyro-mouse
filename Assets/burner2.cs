using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burner2 : MonoBehaviour
{
    public ParticleSystem finalFire;
    public ParticleSystem smoke;
    public AudioSource audioSource; //

    private void Start()
    {
        // Set the particle system to be initially invisible
        finalFire.Stop();
        smoke.Stop(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("oil"))
        {
            // Play the particle system to make it visible
            finalFire.Play();
            smoke.Play();

            // Play the audio clip
            audioSource.Play();
        }
    }
}


