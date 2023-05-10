using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public GameObject player; // Reference to the player game object
    private AudioSource[] allAudioSources; // Array to store all audio sources in the scene
    private bool isPaused = false; // Flag to indicate if the game is currently paused
	public Image pauseImage; // Reference to the pause image on the canvas

    void Start()
    {
        allAudioSources = FindObjectsOfType<AudioSource>(); // Find all audio sources in the scene
		pauseImage.gameObject.SetActive(false); // Hide the pause image on start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGameplay();
            }
        }
    }

    void PauseGameplay()
    {
        isPaused = true;

        // Pause all audio sources
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Pause();
        }

        // Disable player movement
        player.GetComponent<ThirdPersonMovements>().enabled = false;

        // Set the time scale to 0 to pause the game
        Time.timeScale = 0f;
		pauseImage.gameObject.SetActive(true);
    }

    void ResumeGame()
    {
        isPaused = false;

        // Resume all audio sources
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.UnPause();
        }

        // Enable player movement
        player.GetComponent<ThirdPersonMovements>().enabled = true;

        // Set the time scale back to 1 to resume the game
        Time.timeScale = 1f;
		pauseImage.gameObject.SetActive(false);
    }
}

