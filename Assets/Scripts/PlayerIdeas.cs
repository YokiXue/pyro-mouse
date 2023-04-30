using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdeas : MonoBehaviour
{

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


    int[] correctCode = { 7, 6, 4 };
    int[] userCode = new int[3];
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


    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        //doorStartPos1 = door1.transform.position;
        doorStartPos2 = door2.transform.position;
        Debug.Log(doorStartPos2);
        StartCoroutine(ClearPath());
        checkerRoom3.material.color = Color.black;
        
    }

    // Update is called once per frame
    void Update()
    {

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
                break;
            case "press2":
                Debug.Log("pressed on press2");
                t = 2;
                break;
            case "press3":
                Debug.Log("pressed on press3");
                t = 3;
                break;
            case "press4":
                Debug.Log("pressed on press4");
                t = 4;
                break;
            case "press5":
                Debug.Log("pressed on press5");
                t = 5;
                break;
            case "press6":
                Debug.Log("pressed on press6");
                t = 6;
                break;
            case "press7":
                Debug.Log("pressed on press7");
                t = 7;
                break;
            case "press8":
                Debug.Log("pressed on press8");
                t = 8;
                break;
            case "press9":
                Debug.Log("pressed on press9");
                t = 9;
                break;
        };
        if (t == 0) return;
        string resnow = "";

        userCode[currentCode] = t;
        currentCode++;

        //writing the code written by the user into the field
        for (int i = 0; i < currentCode; i++)
        {
            resnow = resnow + userCode[i].ToString();
        }
        //textField.GetComponent<UnityEngine.UI.Text>().text = resnow;
        if (currentCode == 3)
        {
            if (CheckTwoArrays(userCode, correctCode))
            {
                StartCoroutine(checkCode(Color.green));
                currentCode = 0;
                userCode = new int[3];
                StartCoroutine(OpenDoor(door2, doorStartPos2, doorEndPos2));
            }
            else
            {
                StartCoroutine(checkCode(Color.red));
                currentCode = 0;
                userCode = new int[3];

            }

        }
    }
   

    public void OnClick(int t)
    {
        string resnow = "";
        
        userCode[currentCode] = t;
        currentCode++;

        //writing the code written by the user into the field
        for (int i = 0; i < currentCode; i++)
        {
            resnow = resnow + userCode[i].ToString();
        }
        textField.GetComponent<UnityEngine.UI.Text>().text = resnow;
        if (currentCode == 3)
        {
            if (CheckTwoArrays(userCode, correctCode))
            {
                StartCoroutine(checkCode(Color.green));
                StartCoroutine(OpenDoor(door2, doorStartPos2, doorEndPos2));
                currentCode = 0;
                userCode = new int[3];
            } else
            {
                StartCoroutine(checkCode(Color.red));
                currentCode = 0;
                userCode = new int[3];
                
            }
            
        }
    }
    bool CheckTwoArrays(int[] ar1, int[] ar2)
    {
        for (int i = 0; i < 3; i++)
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
}