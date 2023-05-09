using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdeas : MonoBehaviour
{


    public AudioSource newgame; // 
    public AudioSource task1;
    public AudioSource task2;
    public AudioSource task3near;
    public AudioSource task3away;
    public AudioSource task4;
    public AudioSource ending;

    public AudioSource door;

    bool task1Done = false;
    bool task2Done = false;
    bool task3Done = false;
    bool task4Done = false;

    bool playFirst = false;
    bool playNext = true;
    bool playRatCage = true;

    public MeshRenderer meshRenderer1;
    public MeshRenderer meshRenderer2;
    public MeshRenderer meshRenderer3;
    public MeshRenderer meshRenderer4;
    public MeshRenderer meshRenderer5;
    public MeshRenderer meshRenderer6;
    public MeshRenderer meshRenderer7;
    public MeshRenderer meshRenderer8;
    public MeshRenderer meshRenderer9;


    int[] corectPath = { 1, 2, 3, 4 };
    int current;


    int[] correctCode = { 1, 9, 2, 8 };
    int[] userCode = new int[4];
    int currentCode = 0;
    public MeshRenderer checkerRoom3;
    public GameObject textField;

    //public GameObject door1;
    public GameObject door2;

    //private Vector3 doorStartPos1;
    //public Vector3 doorEndPos1;
    private Vector3 doorStartPos2;
    public Vector3 doorEndPos2;
    //public AudioSource beepSound;
    //public AudioSource doorSound;

    private float openDoorElapsedTime = 0.0f;
    private float openDoorDuration = 4.0f;
    float lerpValue;
    bool isFirstRun = true;


    // TASK 2
    // WIRE CUTTING
    public float moveDistance = 0.1f; // How much to move the object each time
    public int requiredClicks = 5; // How many clicks are required before the object moves
    public float timeLimit = 5f;
    public GameObject objectToMove; // Reference to the object that will be moved
    public AudioSource audioSource; //
    

    private int clickCount = 0;
    private Coroutine clickCoroutine;


    //TASK 3

    public Rigidbody cage;

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        //doorStartPos1 = door1.transform.position;
        doorStartPos2 = door2.transform.position;
        Debug.Log(doorStartPos2);
        StartCoroutine(ClearPath());
        checkerRoom3.material.color = Color.black;
        //myAnimator = GetComponent<Animator>();
        StartCoroutine(playAudio(newgame));

        //task1.Play();

    }

    // Update is called once per frame
    void Update()
    {
        // playing the second audio right after the first
        if (newgame.isPlaying == false && playNext && playFirst)
        {
            playNext = false;
            Debug.Log("yes");
            StartCoroutine(playAudio(task1));
        }

        // Check if the player is inside the wire area and presses the "E" key
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerInWireArea() && task1Done && task2Done == false)
        {

            //ADD CHEWING AUDIO HERE
            audioSource.Play();

            clickCount++;
            // If the required number of clicks has been reached, move the object
            if (clickCount == requiredClicks)
            {
                task2Done = true;
                StartCoroutine(playAudio(task3away));
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

    IEnumerator playAudio(AudioSource asa){
        yield return new WaitForSeconds(3);
        asa.Play();
        playFirst = true;
    }

    // TASK 1
    IEnumerator buttonPressed(MeshRenderer mr)
    {
        mr.material.color = Color.blue;
        yield return new WaitForSeconds(2);
        mr.material.color = Color.gray;
    }

    IEnumerator ClearPath()
    {
        meshRenderer1.material.color = Color.red;
        meshRenderer2.material.color = Color.red;
        meshRenderer3.material.color = Color.red;
        meshRenderer4.material.color = Color.red;
        meshRenderer5.material.color = Color.red;
        meshRenderer6.material.color = Color.red;
        meshRenderer7.material.color = Color.red;
        meshRenderer8.material.color = Color.red;
        meshRenderer9.material.color = Color.red;
        yield return new WaitForSeconds(2);
        meshRenderer1.material.color = Color.gray;
        meshRenderer2.material.color = Color.gray;
        meshRenderer3.material.color = Color.gray;
        meshRenderer4.material.color = Color.gray;
        meshRenderer5.material.color = Color.gray;
        meshRenderer6.material.color = Color.gray;
        meshRenderer7.material.color = Color.gray;
        meshRenderer8.material.color = Color.gray;
        meshRenderer9.material.color = Color.gray;
        current = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        tag = other.tag;
        Debug.Log(tag);
        //Debug.Log(p);÷
        //if (tag == "press1") Debug.Log("HELLO");
        //else Debug.Log("Nope");
        int t = 0;
        switch (tag)
        {
            case "press1":
                Debug.Log("pressed on press1");
                t = 1;
                StartCoroutine(buttonPressed(meshRenderer1));
                break;
            case "press2":
                Debug.Log("pressed on press2");
                t = 2;
                StartCoroutine(buttonPressed(meshRenderer2));
                break;
            case "press3":
                Debug.Log("pressed on press3");
                t = 3;
                StartCoroutine(buttonPressed(meshRenderer3));
                break;
            case "press4":
                Debug.Log("pressed on press4");
                t = 4;
                StartCoroutine(buttonPressed(meshRenderer4));
                break;
            case "press5":
                Debug.Log("pressed on press5");
                t = 5;
                StartCoroutine(buttonPressed(meshRenderer5));
                break;
            case "press6":
                Debug.Log("pressed on press6");
                t = 6;
                StartCoroutine(buttonPressed(meshRenderer6));
                break;
            case "press7":
                Debug.Log("pressed on press7");
                t = 7;
                StartCoroutine(buttonPressed(meshRenderer7));
                break;
            case "press8":
                Debug.Log("pressed on press8");
                t = 8;
                StartCoroutine(buttonPressed(meshRenderer8));
                break;
            case "press9":
                Debug.Log("pressed on press9");
                t = 9;
                StartCoroutine(buttonPressed(meshRenderer9));
                break;
            case "ratCage":
                if (task2Done && playRatCage)
                {
                    playRatCage = false;
                    cage.isKinematic = false;
                    StartCoroutine(playAudio(task3near));

                }
                break;

        };
        if (t == 0) return;
        if (task1Done) return;
        string resnow = "";

        userCode[currentCode] = t;
        currentCode++;

        //writing the code written by the user into the field
        for (int i = 0; i < currentCode; i++)
        {
            resnow = resnow + userCode[i].ToString();
        }
        //textField.GetComponent<UnityEngine.UI.Text>().text = resnow;
        if (currentCode == 4)
        {
            if (CheckTwoArrays(userCode, correctCode))
            {
                StartCoroutine(checkCode(Color.green));
                door.Play();
                currentCode = 0;
                userCode = new int[4];
                StartCoroutine(OpenDoor(door2, doorStartPos2, doorEndPos2));
                task1Done = true;
                StartCoroutine(playAudio(task2));

            }
            else
            {
                StartCoroutine(checkCode(Color.red));
                StartCoroutine(ClearPath());
                currentCode = 0;
                userCode = new int[4];

            }

        }
    }
   

    
    bool CheckTwoArrays(int[] ar1, int[] ar2)
    {
        for (int i = 0; i < 4; i++)
        {
            if (ar1[i] != ar2[i]) return false;
        }
        return true;
    }
    IEnumerator checkCode(Color clr)
    {
        checkerRoom3.material.color = clr;
        yield return new WaitForSeconds(2);
        checkerRoom3.material.color = Color.black;
    }

    IEnumerator OpenDoor(GameObject doorObject, Vector3 doorStartPos, Vector3 doorEndPos)
    {
        if (isFirstRun)
        { // delays door opening by 2 seconds
            yield return new WaitForSeconds(2);
            isFirstRun = false;
        }

        //doorSound.Play();
        while (true)
        {
            if (openDoorElapsedTime < openDoorDuration)
            {
                doorObject.transform.localPosition = Vector3.MoveTowards(doorStartPos, doorEndPos, openDoorElapsedTime / openDoorDuration);
                openDoorElapsedTime += Time.deltaTime;
            }
            else if (openDoorElapsedTime > openDoorDuration)
            {
                isFirstRun = true;
                StopCoroutine(OpenDoor(doorObject, doorStartPos, doorEndPos));
                openDoorElapsedTime = 0.0f;
                openDoorDuration = 4.0f;
            }
            yield return null;
        }

    }


    // TASK 2
    // Check if the player is inside the wire area
    bool IsPlayerInWireArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f); // Change the radius as needed
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