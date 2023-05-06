using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRats : MonoBehaviour
{
    private bool collided = false; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter (Collision collision){
        if(collision.gameObject.CompareTag ("ground"))
        {
            collided = true; 

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
