using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireCutting : MonoBehaviour
{
    public float moveDistance = 0.1f; // How much to move the object each time
    public int requiredClicks = 5; // How many clicks are required before the object moves
	public float timeLimit = 5f;
    public GameObject objectToMove; // Reference to the object that will be moved

    private int clickCount = 0;
	 private Coroutine clickCoroutine;

    // Update is called once per frame
    void Update()
    {
        // Check if the player is inside the wire area and presses the "E" key
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerInWireArea())
        {
            clickCount++;
            // If the required number of clicks has been reached, move the object
            if (clickCount == requiredClicks)
            {
                objectToMove.transform.position += new Vector3(moveDistance, 0, 0);
                clickCount = 0;
				 if (clickCoroutine != null)
                {
                    StopCoroutine(clickCoroutine);
                }
            }
			 else if (clickCoroutine == null)
            {
                clickCoroutine = StartCoroutine(ResetClickCount());
            }
        }
    }

    // Check if the player is inside the wire area
    bool IsPlayerInWireArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f); // Change the radius as needed
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("wire"))
            {
                return true;
            }
        }
        return false;
    }
	
	 IEnumerator ResetClickCount()
    {
        yield return new WaitForSeconds(timeLimit);
        clickCount = 0;
        clickCoroutine = null;
    }
}

