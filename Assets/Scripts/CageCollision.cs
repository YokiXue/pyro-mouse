using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageCollision : MonoBehaviour
{
    public AudioSource hitCageSource;
    public AudioSource task4;

    bool hit = false;

    public Rigidbody fire;

    // Start is called before the first frame update
    void Start()
    {
        hitCageSource = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter (Collision collision){
        if(collision.gameObject.tag == "ground" && hit == false){
            hitCageSource.Play();
            StartCoroutine(playAudio(task4));
            fire.isKinematic = false;
            hit = true;
        }
    }
    IEnumerator playAudio(AudioSource asa)
    {
        yield return new WaitForSeconds(3);
        asa.Play();
    }
}
