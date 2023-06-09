using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class burner2 : MonoBehaviour
{
    public ParticleSystem finalFire;
    public ParticleSystem smoke;
    public AudioSource audioSource; //

    private float audioTimer = 0f;

    public AudioSource ending; //

    bool hit = false;
    bool audioStart = false;

    private void Start()
    {
        // Set the particle system to be initially invisible
        finalFire.Stop();
        smoke.Stop(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("oil") && hit == false)
        {
            // Play the particle system to make it visible
            finalFire.Play();
            smoke.Play();
            // Play the audio clip
            audioSource.Play();
            ending.Play();
            //StartCoroutine(playAudio(ending));
            
        }
    }

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            audioTimer += Time.deltaTime;
        }

        if (ending.isPlaying == false && audioSource.clip != null && audioTimer>=25f )
        {
            //StartCoroutine(moveToBlackScreen());
            SceneManager.LoadScene("LastScene");
        }
    }

    IEnumerator playAudio(AudioSource asa)
    {
        yield return new WaitForSeconds(3);
        asa.Play();
        audioStart = true;
    }

    IEnumerator moveToBlackScreen()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("LastScene");
    }
}



