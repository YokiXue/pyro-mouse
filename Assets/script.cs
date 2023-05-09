using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(moveToMenu());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator moveToMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("open");
    }
}
