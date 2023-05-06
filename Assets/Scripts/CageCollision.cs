using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageCollision : MonoBehaviour
{
    public AudioSource hitCageSource;

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
        if(collision.gameObject.tag == "ground"){
            hitCageSource.Play();
        }
    }
}
